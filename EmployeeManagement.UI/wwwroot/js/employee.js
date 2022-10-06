$(document).ready(function () {
    bindEvents();
    hideEmployeeDetailCard();
});


function bindEvents() {
    $(".employeeDetails").on("click", function (event) {
        var employeeId = event.currentTarget.getAttribute("data-id");

        $.ajax({
            url: 'https://localhost:44383/api/internal/employees/' + employeeId,
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                var newEmployeeCard = `<div class="card">
                                          <h1>${result.name}</h1>
                                         <b>Id :</b> <p>${result.id}</p>                                      
                                         <b>Department_Name:</b><p>${result.department_Name}</p>
                                         <b>Age:</b><p>${result.age}</p>                                        
                                         <b>Address:</b><p>${result.address}</p>
                                         <b>Employee_Id:</b><p>${result.employee_Id}</p>
                                        </div>`

                $("#EmployeeCard").html(newEmployeeCard);
                showEmployeeDetailCard();
            },
            error: function (error) {
                console.log(error);
            }
        });
    });


    $("#createform").submit(function (event) {
      

        var employeeDetailedViewModel = {};

        employeeDetailedViewModel.Name = $("#name").val();
        employeeDetailedViewModel.Employee_Id = Number($("#employee_Id").val());
        employeeDetailedViewModel.Department_Id = Number($("#department_Id").val());
        employeeDetailedViewModel.Department_Name = $("#department_Name").val();
        employeeDetailedViewModel.Age = Number($("#age").val());
        employeeDetailedViewModel.Address = $("#address").val();
       

        var data = JSON.stringify(employeeDetailedViewModel);

        $.ajax({
            url: 'https://localhost:44383/api/internal/insertEmployees',
            type: 'POST',
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: data,
            async: false,
            success: function (result) {

                location.reload();
            },
            error: function (error) {
                console.log(error);
            }
        });
    });

    $(".employeeEdit").on("click", function (event) {
        console.log("clicked");
        var employeeId = event.currentTarget.getAttribute("data-id");


        $.ajax({
            url: 'https://localhost:44383/api/internal/employees/' + employeeId,
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            success: function (result) {

                $("#nameEdit").val(result.name)
                Number($("#employee_IdEdit").val(result.employee_Id))
                Number($("#department_IdEdit").val(result.department_Id))
                Number($("#ageEdit").val(result.age))
                $("#addressEdit").val(result.address)

            },
            error: function (error) {
                console.log(error);
            }
        });

        $("#Updateform").submit(function (event) {
            console.log("clicked");

            var employeeDetailedViewModel = {};

            employeeDetailedViewModel.Name = $("#nameEdit").val();
            employeeDetailedViewModel.Employee_Id = Number($("#employee_IdEdit").val());
            employeeDetailedViewModel.Department_Id = Number($("#department_IdEdit").val());
            employeeDetailedViewModel.Age = Number($("#ageEdit").val());
            employeeDetailedViewModel.Address = $("#addressEdit").val();
           

            var data = JSON.stringify(employeeDetailedViewModel);

            $.ajax({
                url: 'https://localhost:44383/api/internal/updateEmployees',
                type: 'PUT',
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: data,
                async: false,
                success: function (result) {
                    location.reload();
                },
                error: function (error) {
                    console.log(error);
                }
            });
        });
    });

    $(".employeeDelete").on("click", function (event) {
        var employeeId = event.currentTarget.getAttribute("data-id");

        var result = confirm("OK OR Cancel?");
        if (result) {
            alert("Successfully deleted the data");
            $.ajax({
                url: 'https://localhost:44383/api/internal/deleteEmployees/' + employeeId,
                type: 'DELETE',
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    location.reload();

                    $("#EmployeeCard").html(newEmployeeCard);
                    showEmployeeDetailCard();
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
        else {
            alert("Not deleted");
        }
    });
}
        

 
function hideEmployeeDetailCard() {
            $("#EmployeeCard").hide();
        }

function showEmployeeDetailCard() {
            $("#EmployeeCard").show();
        }

function showemployeeEditCard() {
            $("#EmployeeCard").show();
        }

function showemployeeDeleteCard() {
            $("#EmployeeCard").show();
        }