$(document).ready(function () {
    ko.applyBindings(new customerVM(), document.getElementById('displayNode'));
    ko.applyBindings(new customer(), document.getElementById('createNode'));
});

function customer(name, age, comments) {
    var self = this;


    self.Name = name;
    self.Age = age,
        self.Comments = comments;

    self.addCustomer = function () {
        alert("ok");
        //$.ajax({
        //    url: "/api/customer/",
        //    type: 'post',
        //    data: ko.toJSON(this),
        //    contentType: 'application/json',
        //    success: function (result) {
        //    }
        //});
    }
}