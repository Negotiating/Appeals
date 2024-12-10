document.addEventListener('DOMContentLoaded', function() {
    // Пример данных обращений
    let appeals = [
        { theme: 'Водоснабжение', title: 'Проблема с водоснабжением', status: 'Черновик', creationDate: '2023-10-01', date: new Date('2023-10-01') },
        { theme: 'Ремонт', title: 'Ремонт крыши', status: 'Зарегистрировано', creationDate: '2023-10-02', date: new Date('2023-10-02') },
        { theme: 'Освещение', title: 'Проблема с освещением', status: 'На проверке', creationDate: '2023-10-03', date: new Date('2023-10-03') },
        { theme: 'Отопление', title: 'Проблема с отоплением', status: 'К исполнению', creationDate: '2023-10-04', date: new Date('2023-10-04') },
        { theme: 'Канализация', title: 'Проблема с канализацией', status: 'В работе', creationDate: '2023-10-05', date: new Date('2023-10-05') },
        { theme: 'Общественный транспорт', title: 'Проблема с общественным транспортом', status: 'Выполнено', creationDate: '2023-10-06', date: new Date('2023-10-06') },
        { theme: 'Уборка территории', title: 'Проблема с уборкой территории', status: 'Отклонено', creationDate: '2023-10-07', date: new Date('2023-10-07') }
    ];

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

    // Функция для фильтрации и сортировки обращений
    function filterAppeals() {
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
            const matchesStatus = statusFilter === '' || appeal.status === statusFilter;
            const matchesDate = (!startDate || new Date(appeal.creationDate) >= startDate) &&
                                (!endDate || new Date(appeal.creationDate) <= endDate);
            return matchesStatus && matchesDate;
        });

        filteredAppeals.sort((a, b) => new Date(b.creationDate) - new Date(a.creationDate));

        filteredAppeals.forEach(appeal => {
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${appeal.theme}</td>
                <td>${appeal.title}</td>
                <td><span class="status-badge status-${appeal.status.toLowerCase()}">${appeal.status}</span></td>
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
    document.getElementById('addAppealForm').addEventListener('submit', function(event) {
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

        appeals.push(newAppeal);
        filterAppeals();
        document.getElementById('addAppealModal').style.display = 'none';
        document.getElementById('addAppealForm').reset();
    });

    // Функция для открытия модального окна с данными обращения
    function openModal(appeal) {
        document.getElementById('modalTitle').innerText = appeal.title; // Устанавливаем заголовок модального окна
        document.getElementById('theme').value = appeal.theme;
        document.getElementById('title').value = appeal.title;
        document.getElementById('status').value = appeal.status;
        document.getElementById('creationDate').value = appeal.creationDate;
        document.getElementById('theme').readOnly = true;
        document.getElementById('title').readOnly = true;
        document.getElementById('status').readOnly = true;
        document.getElementById('creationDate').readOnly = true;

        if (appeal.status === 'Черновик') {
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
    document.getElementById('saveButton').addEventListener('click', function() {
        const appeal = {
            theme: document.getElementById('theme').value,
            title: document.getElementById('title').value,
            status: 'Черновик',
            creationDate: document.getElementById('creationDate').value,
            date: new Date(document.getElementById('creationDate').value)
        };

        appeals.push(appeal);
        filterAppeals();
        document.getElementById('addAppealModal').style.display = 'none';
    });

    // Обработчик событий для кнопки "Отправить"
    document.getElementById('sendButton').addEventListener('click', function() {
        const appeal = {
            theme: document.getElementById('theme').value,
            title: document.getElementById('title').value,
            status: 'Зарегистрировано',
            creationDate: document.getElementById('creationDate').value,
            date: new Date(document.getElementById('creationDate').value)
        };

        appeals.push(appeal);
        filterAppeals();
        document.getElementById('addAppealModal').style.display = 'none';
    });

    // Обработчик событий для кнопки "Отмена"
    document.getElementById('cancelButton').addEventListener('click', function() {
        document.getElementById('addAppealModal').style.display = 'none';
    });

    // Обработчик событий для кнопки "Удалить"
    document.getElementById('deleteButton').addEventListener('click', function() {
        if (confirm('Вы действительно хотите удалить это обращение?')) {
            const appealIndex = appeals.findIndex(appeal => appeal.creationDate === document.getElementById('creationDate').value);
            if (appealIndex !== -1) {
                appeals.splice(appealIndex, 1);
                filterAppeals();
                document.getElementById('addAppealModal').style.display = 'none';
            }
        }
    });
});
