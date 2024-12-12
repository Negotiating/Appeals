document.addEventListener('DOMContentLoaded', function () {
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

    // Функция для загрузки тем с сервера
    async function loadThemes() {
        try {
            const response = await fetch('/Appeals/GetAllTopics');
            if (!response.ok) {
                throw new Error(`Error: ${response.status}`);
            }
            const themes = await response.json();
            return themes;
        } catch (error) {
            showErrorMessage(error.message);
            return [];
        }
    }

    // Функция для загрузки статусов с сервера
    async function loadStatuses() {
        try {
            const response = await fetch('/Appeals/GetAllStatuses');
            if (!response.ok) {
                throw new Error(`Error: ${response.status}`);
            }
            const statuses = await response.json();
            return statuses;
        } catch (error) {
            showErrorMessage(error.message);
            return [];
        }
    }

    // Функция для заполнения выпадающего списка тем
    async function populateThemeDropdown() {
        const themes = await loadThemes();
        const themeDropdown = document.getElementById('theme');
        themeDropdown.innerHTML = '';

        themes.forEach(theme => {
            const option = document.createElement('option');
            option.value = theme.id;
            option.textContent = theme.name;
            themeDropdown.appendChild(option);
        });
    }

    // Функция для заполнения выпадающего списка статусов
    async function populateStatusDropdown() {
        const statuses = await loadStatuses();
        const statusDropdown = document.getElementById('statusFilter');
        statusDropdown.innerHTML = '<option value="">Все</option>';

        statuses.forEach(status => {
            const option = document.createElement('option');
            option.value = status.id;
            option.textContent = status.name;
            statusDropdown.appendChild(option);
        });
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
            const matchesStatus = statusFilter === '' || appeal.status.id == statusFilter;
            const matchesDate = (!startDate || new Date(appeal.creationDate) >= startDate) &&
                (!endDate || new Date(appeal.creationDate) <= endDate);
            return matchesStatus && matchesDate;
        });

        // Сортировка по дате создания
        filteredAppeals.sort((a, b) => new Date(b.creationDate) - new Date(a.creationDate));

        filteredAppeals.forEach(appeal => {
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${appeal.topic.name}</td>
                <td>${appeal.title}</td>
                <td><span class="status-badge status-${appeal.status.id}">${appeal.status.name}</span></td>
                <td>${appeal.creationDate}</td>
            `;
            row.addEventListener('click', function () {
                openModal(appeal);
            });
            appealsList.appendChild(row);
        });
    }

    // Функция для обработки изменения фильтра статуса
    window.handleStatusFilterChange = function () {
        filterAppeals();
    };

    // Инициализация фильтрации при загрузке страницы
    filterAppeals();

    // Заполнение выпадающего списка тем при загрузке страницы
    populateThemeDropdown();

    // Заполнение выпадающего списка статусов при загрузке страницы
    populateStatusDropdown();

    // Открытие модального окна
    const addAppealButton = document.getElementById('addAppealButton');
    if (addAppealButton) {
        addAppealButton.addEventListener('click', function () {
            document.getElementById('modalTitle').innerText = 'Добавить новое обращение';
            document.getElementById('addAppealModal').style.display = 'block';
            document.getElementById('theme').disabled = false;
            document.getElementById('title').readOnly = false;
            document.getElementById('content').readOnly = false;
            document.getElementById('statusGroup').style.display = 'none';
            document.getElementById('creationDateGroup').style.display = 'none';
            document.getElementById('saveButton').style.display = 'inline-block';
            document.getElementById('sendButton').style.display = 'inline-block';
            document.getElementById('cancelButton').style.display = 'inline-block';
            document.getElementById('deleteButton').style.display = 'none';
        });
    }

    // Закрытие модального окна
    const closeButton = document.querySelector('.close');
    if (closeButton) {
        closeButton.addEventListener('click', function () {
            document.getElementById('addAppealModal').style.display = 'none';
        });
    }

    // Обработка отправки формы
    const addAppealForm = document.getElementById('addAppealForm');
    if (addAppealForm) {
        addAppealForm.addEventListener('submit', async function (event) {
            event.preventDefault();
            const theme = document.getElementById('theme').value;
            const title = document.getElementById('title').value;
            const content = document.getElementById('content').value;

            const newAppeal = {
                topic: { id: parseInt(theme) },
                title: title,
                text: content,
                status: { id: 2 },
                creationDate: new Date().toISOString().split('T')[0]
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
                    const errorData = await response.json();
                    showErrorMessage(errorData);
                    throw new Error(`Error: ${response.status}`);
                }

                filterAppeals();
                document.getElementById('addAppealModal').style.display = 'none';
                document.getElementById('addAppealForm').reset();
            } catch (error) {
                showErrorMessage(error.message);
            }
        });
    }

    // Функция для открытия модального окна с данными обращения
    function openModal(appeal) {
        document.getElementById('modalTitle').innerText = appeal.title; 
        document.getElementById('theme').value = appeal.topic.id;
        document.getElementById('title').value = appeal.title;
        document.getElementById('content').value = appeal.text;
        document.getElementById('statusBadge').textContent = appeal.status.name;
        document.getElementById('statusBadge').className = `status-badge status-${appeal.status.id}`;
        document.getElementById('creationDate').value = appeal.creationDate;
        document.getElementById('theme').disabled = true; 
        document.getElementById('title').readOnly = true;
        document.getElementById('content').readOnly = true;
        document.getElementById('statusGroup').style.display = 'block';
        document.getElementById('creationDateGroup').style.display = 'block';

        if (appeal.status.id === 1) {
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
    const clearFiltersButton = document.getElementById('clearFiltersButton');
    if (clearFiltersButton) {
        clearFiltersButton.addEventListener('click', function () {
            document.getElementById('statusFilter').value = '';
            document.getElementById('dateRangeFilter').value = '';
            filterAppeals();
        });
    }

    // Обработчик событий для кнопки "Сохранить"
    const saveButton = document.getElementById('saveButton');
    if (saveButton) {
        saveButton.addEventListener('click', async function () {
            const appeal = {
                topic: { id: parseInt(document.getElementById('theme').value) },
                title: document.getElementById('title').value,
                text: document.getElementById('content').value,
                status: { id: 1 },
                creationDate: document.getElementById('creationDate').value,
                date: new Date(document.getElementById('creationDate').value)
            };

            try {
                const response = await fetch('/Appeals/AddAppeal', {
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
    }

    // Обработчик событий для кнопки "Отправить"
    const sendButton = document.getElementById('sendButton');
    if (sendButton) {
        sendButton.addEventListener('click', async function () {
            const appeal = {
                topic: { id: parseInt(document.getElementById('theme').value) },
                title: document.getElementById('title').value,
                text: document.getElementById('content').value,
                status: { id: 2 },
                creationDate: new Date().toISOString().split('T')[0]
            };

            try {
                const response = await fetch('/Appeals/AddAppeal', {
                    method: 'POST',
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
    }

    // Обработчик событий для кнопки "Отмена"
    const cancelButton = document.getElementById('cancelButton');
    if (cancelButton) {
        cancelButton.addEventListener('click', function () {
            document.getElementById('addAppealModal').style.display = 'none';
        });
    }

    // Обработчик событий для кнопки "Удалить"
    const deleteButton = document.getElementById('deleteButton');
    if (deleteButton) {
        deleteButton.addEventListener('click', async function () {
            if (confirm('Вы действительно хотите удалить это обращение?')) {
                const appealId = this.appealId;
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
    }

    // Функция для отображения сообщений об ошибках
    function showErrorMessage(errorData) {
        const errorMessageElement = document.getElementById('errorMessage');
        if (errorMessageElement) {
            errorMessageElement.innerHTML = '';
            for (const key in errorData) {
                if (errorData.hasOwnProperty(key)) {
                    const errorMessages = errorData[key];
                    if (Array.isArray(errorMessages)) {
                        errorMessages.forEach(message => {
                            const p = document.createElement('p');
                            p.textContent = `${key}: ${message}`;
                            errorMessageElement.appendChild(p);
                        });
                    } else {
                        const p = document.createElement('p');
                        p.textContent = `${key}: ${errorMessages}`;
                        errorMessageElement.appendChild(p);
                    }
                }
            }
            errorMessageElement.classList.remove('hidden');
        }
    }

    // Функция для скрытия сообщений об ошибках
    function hideErrorMessage() {
        const errorMessageElement = document.getElementById('errorMessage');
        if (errorMessageElement) {
            errorMessageElement.classList.add('hidden');
        }
    }
});
