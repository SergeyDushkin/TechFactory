angular.module('tfApp', ['mobile-angular-ui']).factory('ProductsFactory', function ($http) {

    var productsCollectionUrl = "http://localhost:5588/odata/Products";

    function query() {
        return $http.get(productsCollectionUrl);
    };

    function save(product) {
        return $http.post(productsCollectionUrl, product);
    };

    return { query: query, save: save };
});