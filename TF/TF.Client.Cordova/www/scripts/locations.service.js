angular.module('locationsApp', []).factory('LocationsService', function ($http) {

    var locationsCollectionUrl = "http://localhost:5588/odata/Locations";

    function query() {
        return $http.get(locationsCollectionUrl);
    };

    function save(location) {
        return $http.post(locationsCollectionUrl, location);
    };

    return { query: query, save: save };
});