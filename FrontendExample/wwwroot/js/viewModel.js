define(['knockout'], function (ko) {
    //private part
    let names = ko.observableArray(
        [{ name: "Peter" }, { name: "Sue" }, { name: "Ellen" }]);
    let firstName = ko.observable("Peter");
    let lastName = ko.observable("Smith");
    let fullName = ko.computed(function() {
        return firstName() + " " + lastName();
    });

    let deleteName = function(data) {
        names.remove(data);
    }

    let clickButton = function() {
        names.push({ name: fullName() });
        firstName("");
        lastName("");

    }


    fetch("api/categories")
        .then(function (response) {
            return response.json();
        })
        .then(function (data) {
            names(data);
        });


    //public part
    return {
        firstName,
        lastName,
        fullName,
        clickButton,
        names,
        deleteName
    };
});