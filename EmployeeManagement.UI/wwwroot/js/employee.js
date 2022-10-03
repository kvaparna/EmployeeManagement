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

        var result = confirm("OK OR Cancel?");
        if (result)
        {
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
        else
        {
            alert("Not deleted");
        }
    });
}


 
function hideEmployeeDetailCard() 
{
    $("#EmployeeCard").hide();
}

function showEmployeeDetailCard() 
{
    $("#EmployeeCard").show();
}

function showemployeeDeleteCard() 
{
    $("#EmployeeCard").show();
}