cconst uri = 'api/Employee';
function getItems() {
    fetch(uri)
        .then(response => response.json())
        .then(data => console.log(data))
        .catch(error => console.error('Unable to get items.', error));
}

function addItem() {


    const employee = {
        id: 8,
        name: 'Yael',
        job: 1,

    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(employee)
    })
        .then(response => response.json())
        .then(() => {
            getItems();
        })
        .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}

function updateItem() {
    const itemId = 3;
    const item = {
        id: 3,
        name: 'Yael',
        job: 2,
    };

    fetch(`${uri}/${itemId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to update item.', error));



    return false;
}