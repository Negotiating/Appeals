document.addEventListener('DOMContentLoaded', function () {
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

    // Инициализация календарей
    flatpickr("#startDateFilter", {
        dateFormat: "Y-m-d",
        onChange: filterAppeals
    });

    flatpickr("#endDateFilter", {
        dateFormat: "Y-m-d",
        onChange: filterAppeals
    });

    flatpickr("#creationDate", {
        dateFormat: "Y-m-d",
    });

    // Функция для фильтрации и сортировки обращений
    function filterAppeals() {
        const statusFilter = document.getElementById('statusFilter').value;
        const startDateFilter = document.getElementById('startDateFilter').value;
        const endDateFilter = document.getElementById('endDateFilter').value;
        const appealsList = document.getElementById('appeals-list');
        appealsList.innerHTML = '';

        const filteredAppeals = appeals.filter(appeal => {
            const matchesStatus = statusFilter === '' || appeal.status === statusFilter;
            const matchesDate = (startDateFilter === '' || new Date(appeal.creationDate) >= new Date(startDateFilter)) &&
                (endDateFilter === '' || new Date(appeal.creationDate) <= new Date(endDateFilter));
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
            row.addEventListener('click', function () {
                openModal(appeal);
            });
            appealsList.appendChild(row);
        });
    }

    // Инициализация фильтрации при загрузке страницы
    filterAppeals();

    // Открытие модального окна
    document.getElementById('addAppealButton').addEventListener('click', function () {
        document.getElementById('modalTitle').innerText = 'Добавить новое обращение';
        document.getElementById('addAppealModal').style.display = 'block';
        document.getElementById('theme').readOnly = false;
        document.getElementById('title').readOnly = false;
        document.getElementById('status').readOnly = false;
        document.getElementById('creationDate').readOnly = false;
    });

    // Закрытие модального окна
    document.querySelector('.close').addEventListener('click', function () {
        document.getElementById('addAppealModal').style.display = 'none';
    });
// Обработка отправки формы
document.getElementById('addAppealForm').addEventListener('submit', function (event) {
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
    document.getElementById('modalTitle').innerText = 'Просмотр обращения';
    document.getElementById('theme').value = appeal.theme;
    document.getElementById('title').value = appeal.title;
    document.getElementById('status').value = appeal.status;
    document.getElementById('creationDate').value = appeal.creationDate;
    document.getElementById('theme').readOnly = true;
    document.getElementById('title').readOnly = true;
    document.getElementById('status').readOnly = true;
    document.getElementById('creationDate').readOnly = true;
    document.getElementById('addAppealModal').style.display = 'block';
}

// Обработчик событий для кнопки сброса фильтра
document.getElementById('clearFiltersButton').addEventListener('click', function () {
    document.getElementById('statusFilter').value = '';
    document.getElementById('startDateFilter').value = '';
    document.getElementById('endDateFilter').value = '';
    filterAppeals();
});
});