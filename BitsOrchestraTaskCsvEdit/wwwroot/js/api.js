let apiServer = "/";

let GetRecords = (onLoad) =>
{
    const xhr = new XMLHttpRequest();
    xhr.onload = () => { onLoad(xhr) };
    xhr.open("GET", apiServer + "Api/GetRecords");
    xhr.send();
}

let EditRecord = (id, dto, onLoad) =>
{
    const xhr = new XMLHttpRequest();
    xhr.onload = () => { onLoad(xhr) };
    xhr.open("PATCH", apiServer + "Api/EditRecord/" + id);
    xhr.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
    xhr.send(JSON.stringify(dto));
}

let DeleteRecord = (id, onLoad) =>
{
    const xhr = new XMLHttpRequest();
    xhr.onload = () => { onLoad(xhr) };
    xhr.open("DELETE", "/Api/DeleteRecord/" + id);
    xhr.send();
}

let UploadCsv = (file, onLoad) =>
{
    let formData = new FormData();
    formData.append("File", file);

    const xhr = new XMLHttpRequest();
    xhr.onload = () => { onLoad(xhr) };
    xhr.open("POST", apiServer + "Api/UploadCsv");
    xhr.send(formData);
}
