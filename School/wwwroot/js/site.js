// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//For Student Entity

$(document).ready(function (event) {
  $('.btn-student--delete, .delete-class--btn').on('click', function (event) {
    const userConfirm = confirm('Are you sure you want to delete this record?');
    if (!userConfirm) {
      event.preventDefault();
      return false;
    }
  });

  //Delete Teacher
  //$('.delete-teacher--btn').on('click', function () {
  //  const tId = $(this).data('teacherid');
  //  console.log(tId);
  //  const userConfirm = confirm('Are you sure you want to delete this record?');
  //  if (userConfirm) {
  //    $.ajax({
  //      url: deleteTeacherUrl,
  //      type: 'POST',
  //      data: {
  //        teacherId: tId
  //      },
  //      success: function (result) {
  //        alert('Teacher Deleted Successfully');
  //        location.reload();
  //      },
  //      error: function (jqXHR, textStatus, errorThrown) {
  //        console.error("AJAX request failed:");
  //        console.error("jqXHR:", jqXHR);
  //      }
  //    });
  //  }
  //});
  function setUpDeleteFunction(selector, dataAttribute, url, successMessage) {
    $(selector).on('click', function () {
      const itemId = $(this).data(dataAttribute);
      const userConfirm = confirm('Are you sure you want to delete this record?');
      if (userConfirm) {
        $.ajax({
          url: url,
          type: 'POST',
          data: {
            id: itemId,
          },
          success: function (result) {
            alert(successMessage);
            location.reload();
          },
          error: function () {
            console.log('Ajax Request failed');
          }
        });
      }
    });
  }

  setUpDeleteFunction('.delete-subject--btn', 'subid', deleteSubjectUrl, 'Subject Deleted Successfully');
  setUpDeleteFunction('.delete-teacher--btn', 'teacherid', deleteTeacherUrl, 'Teacher Deleted Successfully')



  //student date of birth calculation

  $('#dob').on('change', function () {
    const dobStr = $(this).val();
    console.log("Date is: " + dobStr);

    const dobParts = dobStr.split('-');
    const dob = new Date(dobParts[0], dobParts[1] - 1, dobParts[2]);
    console.log("Date object: " + dob);

    const today = new Date();
    let age = today.getFullYear() - dob.getFullYear();
    const month = today.getMonth() - dob.getMonth();

    if (month < 0 || (month === 0 && today.getDate() < dob.getDate())) {
      age--;  // Adjust age if the current month is before the birth month
    }

    console.log("Age is: " + age);
  });


});