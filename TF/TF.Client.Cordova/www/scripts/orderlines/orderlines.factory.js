angular.module('tfApp').factory('OrderLinesFactory', function ($http) {

    var orderLinesCollectionUrl = "http://localhost:5588/odata/OrderLines";

    function query() {
        return $http.get(orderLinesCollectionUrl);
    };

    function save(orderLine) {
        console.log(orderLine);
        return $http.post(orderLinesCollectionUrl, orderLine);
    };

    return { query: query, save: save };
});