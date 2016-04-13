angular.module('locationsApp').directive('locationsList', function (LocationsService) {
    function controller($scope, $rootScope) {

        getLocations();

        $scope.$on("locationsUpdated", getLocations);

        function getLocations() {
            LocationsService.query()
				.then(function (answer) {
				    $scope.locations = answer.data.value;
				}, function (answer) {
				    console.log("Error. Status: " + answer.status + "; StatusText: " + answer.statusText);
				    return [];
				});
        };

        function updateLocations(doc) {
            LocationsService.save(doc)
				.then(function (answer) {
				    $rootScope.$broadcast("locationsUpdated");
				}, function (answer) {
				    console.log("Error. Status: " + answer.status + "; StatusText: " + answer.statusText);
				});
        };

    }

    return {
        restrict: "E",
        templateUrl: "locations.list-template.html",
        controller: controller
    };
});