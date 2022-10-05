$(document).ready(function () {
    bindEvents();
    hideEmployeeDetailCard();
});

function bindEvents() {
    $(".employeeDetails").on("click", function (event) {
        var employeeId = event.currentTarget.getAttribute("data-id");
        

        $.ajax({
            url: 'https://localhost:44383/api/internal/employee/' + employeeId,
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                var newEmployeeCard = `<div class="card">
                                          <h1>${result.name}</h1>
                                         <b>Id :</b> <p>${result.id}</p>
                                         <b>Department:</b><p>${result.department}</p>
                                         <b>Age:</b><p>${result.age}</p>
                                         <b>Address:</b><p>${result.address}</p>
                                        </div>`

                $("#EmployeeCard").html(newEmployeeCard);
                showEmployeeDetailCard();
            },
            error: function (error) {
                console.log(error);
            }
        });
    });

    $(".employeeDelete").on("click", function (event) {
        var employeeId = event.currentTarget.getAttribute("data-id");
        if (confirm('Are you sure you want to delete')) {
            $.ajax({
                url: 'https://localhost:44383/api/internal/employee/deleteEmployees/' + employeeId,
                type: 'DELETE',
                contentType: "application/json; charset=utf-8",
                success: function (result) {

                    location.reload();
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }

    });

    $("#createform").submit( function (event) {
        
        var employeeDetailedViewModel = {};
       
        employeeDetailedViewModel.Name = $("#name").val();
        employeeDetailedViewModel.Department = $("#dept").val();
        employeeDetailedViewModel.Age = Number($("#age").val());
        employeeDetailedViewModel.Address = $("#address").val();
        
        var data = JSON.stringify(employeeDetailedViewModel);
        
        $.ajax({
            url: 'https://localhost:44383/api/internal/employee/insert-employee',
            type: 'POST',
            dataType:'json',
            contentType: "application/json; charset=utf-8",
            data: data,
            success: function (result) {

                location.reload();
            },
            error: function (error) {
                console.log(error);
            }
        });
    });
    

    /*$(".employeeInsert").on("click", function (event) {
        var employeeId = event.currentTarget.getAttribute("data-id");
    $.ajax({
        url: "https://localhost:44383/api/internal/employee/insertEmployees/",
        type: "POST",
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: valdata
    });*/
}

 
function hideEmployeeDetailCard() {
    $("#EmployeeCard").hide();
}

function showEmployeeDetailCard() {
    $("#EmployeeCard").show();
}