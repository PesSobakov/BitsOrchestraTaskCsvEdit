let SendGetRecords = () =>
{
    let onLoad = (xhr) =>
    {
        if (xhr.status == 200) {
            data = JSON.parse(xhr.response);
            updateTable();
        }
        else {
            let errors = JSON.parse(xhr.response).errors;
            if (errors != undefined) {
                let message = "";
                for (let errorKey in errors) {
                    for (let errorValue of errors[errorKey]) {
                        message += errorKey + ": " + errorValue + "<br>";
                    }
                }
                showError(message);
            }
        }

    };
    GetRecords(onLoad);
}

let SendEditRecord = (id, record) =>
{
    let onLoad = (xhr) =>
    {
        if (xhr.status == 200) {
            for (let i = 0; i < data.length; i++) {
                if (data[i].id == id) {
                    data[i] = record;
                }
            }
            updateTable();
        }
        else {
            let errors = JSON.parse(xhr.response).errors;
            if (errors != undefined) {
                let message = "";
                for (let errorKey in errors) {
                    for (let errorValue of errors[errorKey]) {
                        message += errorKey + ": " + errorValue + "<br>";
                    }
                }
                showError(message);
            }
        }
    };
    let dto = {
        Name: record.name,
        DateOfBirth: record.dateOfBirth,
        Married: record.married,
        Phone: record.phone,
        Salary: record.salary
    }
    EditRecord(id, dto, onLoad)
}

let SendDeleteRecord = (id) =>
{
    let onLoad = (xhr) =>
    {
        if (xhr.status == 200) {
            data = data.filter((item) => { return item.id != id });
            updateTable();
        }
        else {
            let errors = JSON.parse(xhr.response).errors;
            if (errors != undefined) {
                let message = "";
                for (let errorKey in errors) {
                    for (let errorValue of errors[errorKey]) {
                        message += errorKey + ": " + errorValue + "<br>";
                    }
                }
                showError(message);
            }
        }
    };
    DeleteRecord(id, onLoad)
}

let SendUploadCsv = () =>
{
    let file = document.getElementById("formFile").files[0];
    let onLoad = (xhr) =>
    {
        if (xhr.status == 200) {
            SendGetRecords();
        }
        else {
            let errors = JSON.parse(xhr.response).errors;
            if (errors != undefined) {
                let message = "";
                for (let errorKey in errors) {
                    for (let errorValue of errors[errorKey]) {
                        message += errorKey + ": " + errorValue + "<br>";
                    }
                }
                showError(message);
            }
        }
    };
    UploadCsv(file, onLoad);
}


let changeSort = (row, desc) =>
{
    sortRow = row;
    sortDesc = desc;
    updateTable();
}

let sortTable = () =>
{
    //dataView = data.copy();
    switch (sortRow) {
        case -1:
            break;
        case 0:
            if (sortDesc) {
                dataView = dataView.sort((a, b) =>
                {
                    if (b.name < a.name)
                        return -1;
                    if (b.name > a.name)
                        return 1;
                    return 0;
                });
            }
            else {
                dataView = dataView.sort((a, b) =>
                {
                    if (a.name < b.name)
                        return -1;
                    if (a.name > b.name)
                        return 1;
                    return 0;
                });
            }
            break;
        case 1:
            if (sortDesc) {
                dataView = dataView.sort((a, b) => { return new Date(b.dateOfBirth) - new Date(a.dateOfBirth) });
            }
            else {
                dataView = dataView.sort((a, b) => { return new Date(a.dateOfBirth) - new Date(b.dateOfBirth) });
            }
            break;
        case 2:
            if (sortDesc) {
                dataView = dataView.sort((a, b) =>
                {
                    return (a.married === b.married ? 0 : a.married ? -1 : 1);
                });
            }
            else {
                dataView = dataView.sort((a, b) =>
                {
                    return (a.married === b.married ? 0 : b.married ? -1 : 1);
                });
            }
            break;
        case 3:
            if (sortDesc) {
                dataView = dataView.sort((a, b) =>
                {
                    if (b.phone < a.phone)
                        return -1;
                    if (b.phone > a.phone)
                        return 1;
                    return 0;
                });
            }
            else {
                dataView = dataView.sort((a, b) =>
                {
                    if (a.phone < b.phone)
                        return -1;
                    if (a.phone > b.phone)
                        return 1;
                    return 0;
                });
            }
            break;

        case 4:
            if (sortDesc) {
                dataView = dataView.sort((a, b) => { return b.salary - a.salary });
            }
            else {
                dataView = dataView.sort((a, b) => { return a.salary - b.salary });
            }
            break;
        default:
            break;
    }
}

let filterTable = () =>
{
    if (filter0.value != "") {
        dataView = dataView.filter((item) => { return item.name.indexOf(filter0.value) >= 0 })
    }
    if (filter1.value != "") {
        dataView = dataView.filter((item) => { return item.dateOfBirth.toString().indexOf(filter1.value) >= 0 })
    }
    if (filter2.value != "") {
        dataView = dataView.filter((item) => { return item.married.toString().indexOf(filter2.value) >= 0 })
    }
    if (filter3.value != "") {
        dataView = dataView.filter((item) => { return item.phone.indexOf(filter3.value) >= 0 })
    }
    if (filter4.value != "") {
        dataView = dataView.filter((item) => { return item.salary.toString().indexOf(filter4.value) >= 0 })
    }
}

let renderTable = () =>
{
    tableData.innerHTML = '';
    for (let i = 0; i < dataView.length; i++) {
        let currentLine = document.createElement("tr");

        let name = document.createElement("td");
        name.innerHTML = dataView[i].name;
        currentLine.appendChild(name);

        let dateOfBirth = document.createElement("td");
        dateOfBirth.innerHTML = dataView[i].dateOfBirth;
        currentLine.appendChild(dateOfBirth);

        let married = document.createElement("td");
        married.innerHTML = dataView[i].married;
        currentLine.appendChild(married);

        let phone = document.createElement("td");
        phone.innerHTML = dataView[i].phone;
        currentLine.appendChild(phone);

        let salary = document.createElement("td");
        salary.innerHTML = dataView[i].salary;
        currentLine.appendChild(salary);

        let editCell = document.createElement("td");
        let editBtn = document.createElement("button");
        editBtn.innerHTML = "edit";
        editBtn.classList = "btn btn-primary";
        editBtn.onclick = () =>
        {
            let record = { ...dataView[i] }
            currentLine.innerHTML = "";

            let nameCell = document.createElement("td");
            let nameInput = document.createElement("input");
            nameInput.type = "text";
            nameInput.value = record.name;
            nameInput.classList = ["form-control"];
            nameCell.appendChild(nameInput);
            currentLine.appendChild(nameCell);

            let dateOfBirthCell = document.createElement("td");
            let dateOfBirthInput = document.createElement("input");
            dateOfBirthInput.type = "text";
            dateOfBirthInput.value = record.dateOfBirth;
            dateOfBirthInput.classList = ["form-control"];
            dateOfBirthCell.appendChild(dateOfBirthInput);
            currentLine.appendChild(dateOfBirthCell);

            let marriedCell = document.createElement("td");
            let marriedInput = document.createElement("input");
            marriedInput.type = "text";
            marriedInput.value = record.married;
            marriedInput.classList = ["form-control"];
            marriedCell.appendChild(marriedInput);
            currentLine.appendChild(marriedCell);

            let phoneCell = document.createElement("td");
            let phoneInput = document.createElement("input");
            phoneInput.type = "text";
            phoneInput.value = record.phone;
            phoneInput.classList = ["form-control"];
            phoneCell.appendChild(phoneInput);
            currentLine.appendChild(phoneCell);

            let salaryCell = document.createElement("td");
            let salaryInput = document.createElement("input");
            salaryInput.type = "text";
            salaryInput.value = record.salary;
            salaryInput.classList = ["form-control"];
            salaryCell.appendChild(salaryInput);
            currentLine.appendChild(salaryCell);

            let confirmCell = document.createElement("td");
            let confirmBtn = document.createElement("button");
            confirmBtn.innerHTML = "confirm";
            confirmBtn.classList = "btn btn-primary";
            confirmBtn.onclick = () =>
            {
                let valid = true;
                nameInput.style.backgroundColor = "#ffffff";
                dateOfBirthInput.style.backgroundColor = "#ffffff";
                marriedInput.style.backgroundColor = "#ffffff";
                phoneInput.style.backgroundColor = "#ffffff";
                salaryInput.style.backgroundColor = "#ffffff";

                if (nameInput.value == "") {
                    valid = false;
                    nameInput.style.backgroundColor = "darksalmon";
                }
                if (dateOfBirthInput.value == "" || isNaN(Date.parse(dateOfBirthInput.value))) {
                    valid = false;
                    dateOfBirthInput.style.backgroundColor = "darksalmon";
                }
                if (!(["", "True", "true", "False", "false"].indexOf(marriedInput.value) != -1)) {
                    valid = false;
                    marriedInput.style.backgroundColor = "darksalmon";
                }
                if (phoneInput.value == "") {
                    valid = false;
                    phoneInput.style.backgroundColor = "darksalmon";
                }
                if (salaryInput.value == "" || isNaN(Number.parseFloat(salaryInput.value))) {
                    valid = false;
                    salaryInput.style.backgroundColor = "darksalmon";
                }
                if (valid) {
                    record.name = nameInput.value;
                    record.dateOfBirth = new Date(dateOfBirthInput.value).toISOString().slice(0, 10);
                    record.married = ["True", "true"].indexOf(marriedInput.value) != -1 ? true : false;
                    record.phone = phoneInput.value;
                    record.salary = Number.parseFloat(salaryInput.value);

                    SendEditRecord(record.id, record);
                }
            };
            confirmCell.appendChild(confirmBtn);
            currentLine.appendChild(confirmCell);

            let cancelCell = document.createElement("td");
            let cancelBtn = document.createElement("button");
            cancelBtn.innerHTML = "cancel";
            cancelBtn.classList = "btn btn-primary";
            cancelBtn.onclick = () =>
            {
                updateTable();
            };
            cancelCell.appendChild(cancelBtn);
            currentLine.appendChild(cancelCell);

        }
        editCell.appendChild(editBtn);
        currentLine.appendChild(editCell);

        let deleteCell = document.createElement("td");
        let deleteBtn = document.createElement("button");
        deleteBtn.innerHTML = "delete";
        deleteBtn.classList = "btn btn-primary";
        deleteBtn.onclick = () =>
        {
            SendDeleteRecord(dataView[i].id)
        };
        deleteCell.appendChild(deleteBtn);
        currentLine.appendChild(deleteCell);

        tableData.appendChild(currentLine);
    }
}

let updateTable = () =>
{
    dataView = [...data];
    sortTable();
    filterTable();
    renderTable();
}

let showError = (message) =>
{
    errorMessage.innerHTML = message;
    bsAlert.show();
}


let data = [];
SendGetRecords();
let dataView = data;
let tableHead = document.getElementById("head");
let tableData = document.getElementById("data");
let sortRow = -1;
let sortDesc = false;
let filter0 = document.getElementById("filter0");
let filter1 = document.getElementById("filter1");
let filter2 = document.getElementById("filter2");
let filter3 = document.getElementById("filter3");
let filter4 = document.getElementById("filter4");

let errorMessage = document.getElementById('ErrorMessage');
let myAlert = document.getElementById('ErrorToast');
let bsAlert = new bootstrap.Toast(myAlert);

