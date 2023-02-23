var host = "https://localhost:";
var port = "44343/";
var loginEndpoint ="api/authentication/login";
var registerEndpoint = "api/authentication/register";
var sellersEndpoint = "api/sellers/";
var shopsEndpoint = "api/shops/";
var jwt_token;
var editingId;
var formAction = "Create";

function loadPage()
{
    loadSellers();
}

function showLogin()
{
    document.getElementById("formDiv").style.display = "none";
    document.getElementById("logout").style.display = "none";
    document.getElementById("loginFormDiv").style.display ="block";
    document.getElementById("registerFormDiv").style.display = "none";
}

function showRegistration()
{
    document.getElementById("formDiv").style.display = "none";
    document.getElementById("logout").style.display = "none";
    document.getElementById("registerFormDiv").style.display = "block";
    document.getElementById("loginFormDiv").style.display = "none";
}

function registerUser()
{
    var username = document.getElementById("usernameRegister").value;
    var email = document.getElementById("emailRegister").value;
    var password = document.getElementById("passwordRegister").value;
    var confirmPassword = document.getElementById("confirmPasswordRegister").value;

    if(validateRegisterForm(username, email, password, confirmPassword))
    {
        var url = host + port + registerEndpoint;
        var sendData = {
            "Username" : username,
            "Password" : password, 
            "Email" : email
        };

        console.log("Url zahteva: ");
        console.log(url);

        fetch(url, {method: "POST", headers: {'Content-Type': 'application/json'}, body: JSON.stringify(sendData)})
        .then((response)=>
        {
            if(response.status === 200)
            {
                document.getElementById("registerForm").reset();
                console.log("Successful registration!");
                alert("Successful registration!");
                showLogin();
            }
            else{
                console.log("Error occured with code: " + response.status);
                console.log(response);
                alert("Error occured!");
                response.text().then(text => {console.log(text);})
            }
        }).catch(error => console.log(error));
    }

    return false;
}

function validateRegisterForm(username, email, password, confirmPassword)
{
    if(username.length === 0)
    {
        alert("Username field can not be empty!");
        return false;
    }
    else if(email.length === 0)
    {
        alert("Email field can not be empty!");
        return false;
    }
    else if(password.length === 0)
    {
        alert("Password field can not be empty!");
        return false;
    }
    else if(password !== confirmPassword)
    {
        alert("Password value and confirm password value should match!");
        return false;
    }

    return true;
}

function validateLoginForm(username, password)
{
    if(username.length === 0)
    {
        alert("Username field can not be empty!");
        return false;
    }
    else if(password.length === 0)
    {
        alert("Password field can not be empty!");
        return false;
    }

    return true;
}

function loginUser()
{
    var username = document.getElementById("usernameLogin").value;
    var password = document.getElementById("passwordLogin").value;

    if(validateLoginForm(username, password))
    {
        var url = host +port + loginEndpoint;
        var sendData = {
            "Username" : username,
            "Password" : password
        };

        fetch(url, {method: "POST", headers: {'Content-Type': 'application/json'}, body: JSON.stringify(sendData)})
        .then((response) =>
        {
            if(response.status === 200)
            {
                document.getElementById("loginForm").reset();
                console.log("Successful login");
                alert("Successful login");
                response.json().then(function(data)
                {
                    console.log(data);
                   
                    document.getElementById("info").innerHTML = "Currently logged in user: <i>" + data.username + "</i>.";
                    document.getElementById("logout").style.display = "block";
                    document.getElementById("btnLogin").style.display = "none";
                    document.getElementById("btnRegister").style.display = "none";
                    jwt_token = data.token;
                    loadSellers();
                    loadShopForDropdown();
                });
            }
            else {
                    console.log("Error occured with code " + response.status);
					console.log(response);
					alert("Error occured!");
					response.text().then(text => { console.log(text); })
            }
        }).catch(error => console.log(error));
    }
    return false;
}

function loadSellers()
{
   document.getElementById("loginFormDiv").style.display = "none";
   document.getElementById("registerFormDiv").style.display = "none";

   var requestUrl = host + port + sellersEndpoint;
   console.log("URL zahteva: " + requestUrl);

   var headers = { };
   if(jwt_token)
   {
    headers.Authorization = 'Bearer ' + jwt_token;
   }

   console.log(headers);
   fetch(requestUrl, {headers: headers})
   .then((response) => 
   {
        if(response.status === 200)
        {
            response.json().then(setSellers);
        }
        else
        {
            console.log("Error occured with code " + response.status);
            showError();
        }
   }).catch(error => console.log(error));
}

function showError()
{
    var container = document.getElementById("data");
    container.innerHTML ="";

    var div = document.createElement("div");
    var h1 = document.createElement("h1");
    var errorText = document.createTextNode("Error occured while retrieving data!");
    h1.appendChild(errorText);
    div.appendChild(h1);
    container.appendChild(div);
}

function setSellers(data)
{
    var container = document.getElementById("data");
    container.innerHTML ="";

    console.log(data);

    var div = document.createElement("div");
    var h1 = document.createElement("h1");
    var headingText = document.createTextNode("Sellers");
    h1.appendChild(headingText);
    div.appendChild(h1);

    var table = document.createElement("table");
    table.classList = "table table-hover";

    var header = createHeader();
    table.appendChild(header);

    var tableBody = document.createElement("tbody");

    for(var i = 0; i < data.length; i++)
    {
        var row = document.createElement("tr");
        row.appendChild(createTableCell(data[i].id));
        row.appendChild(createTableCell(data[i].name + " " + data[i].surname));

        if(jwt_token)
        {
            var stringId = data[i].id;
            row.appendChild(createTableCell(data[i].year));
            row.appendChild(createTableCell(data[i].shopName));
            row.appendChild(createTableCell(data[i].shopAddress));

            var btnEdit = document.createElement("button");
            btnEdit.name = stringId;
            btnEdit.addEventListener("click", editSeller);
            btnEdit.classList = "btn btn-warning";
            var btnEditText = document.createTextNode("Edit");
            btnEdit.appendChild(btnEditText);
            var btnEditCell = document.createElement("td");
            btnEditCell.appendChild(btnEdit);
            row.appendChild(btnEditCell);

            var btnDelete = document.createElement("button");
            btnDelete.name = stringId;
            btnDelete.classList = "btn btn-danger";
            btnDelete.addEventListener("click", deleteSeller);
            var btnDeleteText = document.createTextNode("Delete");
            btnDelete.appendChild(btnDeleteText);
            var btnDeleteCell = document.createElement("td");
            btnDeleteCell.appendChild(btnDelete);
            row.appendChild(btnDeleteCell);           
        }

        tableBody.appendChild(row);
    }

    table.appendChild(tableBody);
    div.appendChild(table);

   // document.getElementById("data").style.display = "block";
   if(jwt_token)
   {
    document.getElementById("formDiv").style.display = "block";
   }
    container.appendChild(div);
}

function createTableHeaderCell(text)
{
    var cell = document.createElement("th");
    var cellText = document.createTextNode(text);
    cell.appendChild(cellText);

    return cell;
}

function createTableCell(text)
{
    var cell = document.createElement("td");
    var cellText = document.createTextNode(text);
    cell.appendChild(cellText);

    return cell;
}

function createHeader()
{
    var thead = document.createElement("thead");
    var row = document.createElement("tr");
    row.appendChild(createTableHeaderCell("Id"));
    row.appendChild(createTableHeaderCell("Seller"));

    if(jwt_token)
    {
        row.appendChild(createTableHeaderCell("Year of birth"));
        row.appendChild(createTableHeaderCell("Shop name"));
        row.appendChild(createTableHeaderCell("Shop address"));
        row.appendChild(createTableHeaderCell("Edit"));
        row.appendChild(createTableHeaderCell("Delete"));
    }

    thead.appendChild(row);
    return thead;
}

function loadShopForDropdown()
{
    var requestUrl = host + port + shopsEndpoint;
    console.log("Url zahteva: " + requestUrl);

    var headers = { };
    if(jwt_token)
    {
        headers.Authorization = 'Bearer ' + jwt_token;
    }

    fetch(requestUrl, {headers: headers})
    .then((response) =>
    {
        if(response.status === 200)
        {
            response.json().then(setShopsInDropdown);
        }
        else
        {
            console.log("Error occured with code " + response.status);
        }
    }).catch(error => console.log(error));
   
}
function setShopsInDropdown(data)
{
    console.log(data);
    var dropdown = document.getElementById("sellerShop");
    dropdown.innerHTML = " ";
    for(var i = 0; i< data.length; i++)
    {
        var option = document.createElement("option");
        option.value = data[i].id;
        var text = document.createTextNode(data[i].name);
        option.appendChild(text);
        dropdown.appendChild(option);
    }
}

function editSeller()
{
    var editId = this.name;
    var url = host + port + sellersEndpoint + editId.toString();
    console.log("Url zahteva: " + url);

    var headers = { };
    if(jwt_token)
    {
        headers.Authorization = 'Bearer ' + jwt_token;
    }

    fetch(url, {headers: headers})
    .then((response) => 
    {
        if(response.status === 200)
        {
            console.log("Successful action");
            response.json().then(data => {
                document.getElementById("sellerName").value = data.name;
                document.getElementById("sellerSurname").value = data.surname;
                document.getElementById("sellerYear").value = data.year;
                document.getElementById("sellerShop").value = data.shopId;
                editingId = data.id;
                formAction = "Update";
            });
        }
        else
        {
            formAction ="Create";
            console.log("Error occured with code " + response.status);
		    alert("Error occured!");
        }
    }).catch(error => console.log(error));    
}

function submitSellerForm()
{
    var name = document.getElementById("sellerName").value;
    var surname = document.getElementById("sellerSurname").value;
    var year = document.getElementById("sellerYear").value;
    var shopId = document.getElementById("sellerShop").value;

    var url;
    var sendData;
    var httpAction;

    if(formAction === "Create")
    {
        url = host + port + sellersEndpoint;
        httpAction = "POST";
        sendData = {
            "Name" : name,
            "Surname" : surname,
            "Year" : year,
            "ShopId" : shopId
        };
    }
    else
    {
        url = host + port + sellersEndpoint + editingId.toString();
        httpAction = "PUT";
        sendData = {
            "Id" : editingId,
            "Name" : name,
            "Surname" : surname,
            "Year" : year,
            "ShopId" : shopId
        };
    }
    console.log("Url zahteva: " + url);
    console.log("Objekat za slanje:");
    console.log(sendData);

    var headers = {'Content-Type': 'application/json'};
    if(jwt_token)
    {
        headers.Authorization = 'Bearer ' + jwt_token;
    }

    fetch(url, {method: httpAction, headers: headers, body: JSON.stringify(sendData)})
    .then((response) => {
        if(response.status === 201 || response.status === 200)
        {
            console.log("Successful action!");
            formAction ="Create";
            refreshTable();
        }
        else
        {
            console.log("Error occured with code " + response.status);
            alert("Error occured!");
        }
    }).catch(error => console.log(error));

    return false;
}

function refreshTable()
{
    document.getElementById("sellerForm").reset();
    loadSellers();
}

function deleteSeller()
{
    var deleteId = this.name;
    var url = host + port +  sellersEndpoint + deleteId.toString();
    console.log("Url zahteva: " + url);

    var headers = { };
    if(jwt_token)
    {
        headers.Authorization = 'Bearer ' + jwt_token;
    }

    fetch(url, {method: "DELETE", headers: headers})
    .then((response) => 
    {
        if(response.status === 204)
        {
            console.log("Successful action!");
            refreshTable();
        }
        else 
        {
            console.log("Error occured with code " + response.status);
			alert("Error occured!");
        }
    }).catch(error => console.log(error));
}

function cancelSellerForm()
{
    formAction = "Create";
}

function logout()
{
    jwt_token = undefined;

    document.getElementById("data").innerHTML = " ";
    document.getElementById("info").innerHTML = " ";
    document.getElementById("formDiv").style.display = "none";
    
    document.getElementById("logout").style.display = "none";
    document.getElementById("btnLogin").style.display = "initial";
    document.getElementById("btnRegister").style.display = "initial";
    loadSellers();
}