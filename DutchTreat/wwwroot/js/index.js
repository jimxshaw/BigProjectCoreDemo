$(document).ready(function () {
  $("#theForm").hide();

  var $loginToggle = $("#loginToggle");
  var $popupForm = $(".popupForm");

  $loginToggle.on("click", function() {
    $popupForm.fadeToggle(1000);
  });
});
