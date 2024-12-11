document.addEventListener('DOMContentLoaded', function() {
    // Инициализация календаря для выбора диапазона дат
    flatpickr("#dateRangeFilter", {
        mode: "range",
        dateFormat: "Y-m-d",
        onChange: filterAppeals
    });

    // Инициализация календаря для выбора одной даты
    flatpickr("#creationDate", {
        dateFormat: "Y-m-d",
    });

    // Функция для загрузки данных с сервера
    async function loadAppeals() {
        try {
            const response = await fetch('/Appeals/GetAllAppeals');
            if (!response.ok) {
                throw new Error(`Error: ${response.status}`);
            }
            const appeals = await response.json();
            console.log(appeals);
            return appeals;
        } catch (error) {
            showErrorMessage(error.message);
            return [];
        }
    }

    // Функция для фильтрации и сортировки обращений
    async function filterAppeals() {
        const appeals = await loadAppeals();
        const statusFilter = document.getElementById('statusFilter').value;
        const dateRangeFilter = document.getElementById('dateRangeFilter').value;
        const appealsList = document.getElementById('appeals-list');
        appealsList.innerHTML = '';

        let startDate = null;
        let endDate = null;

        if (dateRangeFilter) {
            const dates = dateRangeFilter.split(' to ');
            if (dates.length === 2) {
                startDate = new Date(dates[0]);
                endDate = new Date(dates[1]);
            } else {
                startDate = new Date(dates[0]);
                endDate = startDate;
            }
        }

        const filteredAppeals = appeals.filter(appeal => {
            const matchesStatus = statusFilter === '' || appeal.idStatusNavigation.name === statusFilter;
            const matchesDate = (!startDate || new Date(appeal.creationDate) >= startDate) &&
                                (!endDate || new Date(appeal.creationDate) <= endDate);
            return matchesStatus && matchesDate;
        });

        // Сортировка по дате создания
        filteredAppeals.sort((a, b) => new Date(b.creationDate) - new Date(a.creationDate));

        filteredAppeals.forEach(appeal => {
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${appeal.Topic.name}</td>
                <td>${appeal.title}</td>
                <td><span class="status-badge status-${appeal.Status.Id.toLowerCase()}">${appeal.Status.name}</span></td>
                <td>${appeal.creationDate}</td>
            `;
            row.addEventListener('click', function() {
                openModal(appeal);
            });
            appealsList.appendChild(row);
        });
    }

    // Инициализация фильтрации при загрузке страницы
    filterAppeals();

    // Открытие модального окна
    document.getElementById('addAppealButton').addEventListener('click', function() {
        document.getElementById('modalTitle').innerText = 'Добавить новое обращение';
        document.getElementById('addAppealModal').style.display = 'block';
        document.getElementById('theme').readOnly = false;
        document.getElementById('title').readOnly = false;
        document.getElementById('status').readOnly = false;
        document.getElementById('creationDate').readOnly = false;
        document.getElementById('saveButton').style.display = 'inline-block';
        document.getElementById('sendButton').style.display = 'inline-block';
        document.getElementById('cancelButton').style.display = 'inline-block';
        document.getElementById('deleteButton').style.display = 'none';
    });

    // Закрытие модального окна
    document.querySelector('.close').addEventListener('click', function() {
        document.getElementById('addAppealModal').style.display = 'none';
    });

    // Обработка отправки формы
    document.getElementById('addAppealForm').addEventListener('submit', async function(event) {
        event.preventDefault();
        const theme = document.getElementById('theme').value;
        const title = document.getElementById('title').value;
        const status = document.getElementById('status').value;
        const creationDate = document.getElementById('creationDate').value;

        const newAppeal = {
            theme: theme,
            title: title,
            status: status,
            creationDate: creationDate,
            date: new Date(creationDate)
        };

        try {
            const response = await fetch('/Appeals/AddAppeal', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(newAppeal)
            });

            if (!response.ok) {
                throw new Error(`Error: ${response.status}`);
            }

            filterAppeals();
            document.getElementById('addAppealModal').style.display = 'none';
            document.getElementById('addAppealForm').reset();
        } catch (error) {
            showErrorMessage(error.message);
        }
    });

    // Функция для открытия модального окна с данными обращения
    function openModal(appeal) {
        document.getElementById('modalTitle').innerText = appeal.title; // Устанавливаем заголовок модального окна
        document.getElementById('theme').value = appeal.idTopicNavigation.name;
        document.getElementById('title').value = appeal.title;
        document.getElementById('status').value = appeal.idStatusNavigation.name;
        document.getElementById('creationDate').value = appeal.creationDate;
        document.getElementById('theme').readOnly = true;
        document.getElementById('title').readOnly = true;
        document.getElementById('status').readOnly = true;
        document.getElementById('creationDate').readOnly = true;

        if (appeal.idStatusNavigation.name === 'Черновик') {
            document.getElementById('saveButton').style.display = 'inline-block';
            document.getElementById('sendButton').style.display = 'none';
        } else {
            document.getElementById('saveButton').style.display = 'none';
            document.getElementById('sendButton').style.display = 'none';
        }

        document.getElementById('cancelButton').style.display = 'inline-block';
        document.getElementById('deleteButton').style.display = 'inline-block';

        document.getElementById('addAppealModal').style.display = 'block';
    }

    // Обработчик событий для кнопки сброса фильтра
    document.getElementById('clearFiltersButton').addEventListener('click', function() {
        document.getElementById('statusFilter').value = '';
        document.getElementById('dateRangeFilter').value = '';
        filterAppeals();
    });

    // Обработчик событий для кнопки "Сохранить"
    document.getElementById('saveButton').addEventListener('click', async function() {
        const appeal = {
            theme: document.getElementById('theme').value,
            title: document.getElementById('title').value,
            status: 'Черновик',
            creationDate: document.getElementById('creationDate').value,
            date: new Date(document.getElementById('creationDate').value)
        };

        try {
            const response = await fetch('/Appeals/UpdateAppeal', {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(appeal)
            });

            if (!response.ok) {
                throw new Error(`Error: ${response.status}`);
            }

            filterAppeals();
            document.getElementById('addAppealModal').style.display = 'none';
        } catch (error) {
            showErrorMessage(error.message);
        }
    });

    // Обработчик событий для кнопки "Отправить"
    document.getElementById('sendButton').addEventListener('click', async function() {
        const appeal = {
            theme: document.getElementById('theme').value,
            title: document.getElementById('title').value,
            status: 'Зарегистрировано',
            creationDate: document.getElementById('creationDate').value,
            date: new Date(document.getElementById('creationDate').value)
        };

        try {
            const response = await fetch('/Appeals/UpdateAppeal', {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(appeal)
            });

            if (!response.ok) {
                throw new Error(`Error: ${response.status}`);
            }

            filterAppeals();
            document.getElementById('addAppealModal').style.display = 'none';
        } catch (error) {
            showErrorMessage(error.message);
        }
    });

    // Обработчик событий для кнопки "Отмена"
    document.getElementById('cancelButton').addEventListener('click', function() {
        document.getElementById('addAppealModal').style.display = 'none';
    });

    // Обработчик событий для кнопки "Удалить"
    document.getElementById('deleteButton').addEventListener('click', async function() {
        if (confirm('Вы действительно хотите удалить это обращение?')) {
            const appealId = document.getElementById('creationDate').value;
            try {
                const response = await fetch(`/Appeals/DeleteAppeal/${appealId}`, {
                    method: 'DELETE'
                });

                if (!response.ok) {
                    throw new Error(`Error: ${response.status}`);
                }

                filterAppeals();
                document.getElementById('addAppealModal').style.display = 'none';
            } catch (error) {
                showErrorMessage(error.message);
            }
        }
    });

    // Функция для отображения сообщений об ошибках
    function showErrorMessage(message) {
        const errorMessageElement = document.getElementById('errorMessage');
        errorMessageElement.innerText = message;
        errorMessageElement.classList.remove('hidden');
    }

    // Функция для скрытия сообщений об ошибках
    function hideErrorMessage() {
        const errorMessageElement = document.getElementById('errorMessage');
        errorMessageElement.classList.add('hidden');
    }
});
