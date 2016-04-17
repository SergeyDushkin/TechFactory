angular.module('tfApp').directive('productsList', function (ProductsFactory, OrderLinesFactory, $filter) {
    function controller($scope, $rootScope) {

        $scope.picked = [];
        $scope.products = [];

        $scope.productClick = productClick;
        $scope.order = order;

        getProducts();

        function getProducts() {
            ProductsFactory.query()
				.then(function (answer) {
				    $scope.products = answer.data.value;
				}, onError);
        };

        function productClick(product) {
            if ($scope.picked.indexOf(product) >= 0) {
                $scope.picked = $filter('filter')($scope.picked, { Name: '!' + product.Name }, true)
            }
            else {
                $scope.picked.push(product);
            }
        };

        //TODO: убрать это отсюда, заменить пик на событие?
        function order() {
            for (var productIdx in $scope.picked) {
                var orderLine = {
                    BaseQty: 1,
                    Qty: 1,
                    ItemId: $scope.picked[productIdx].Id,
                    OrderId: "8ddcf8e9-7c89-4253-879a-16b52e598f65",
                    Priority: 1,
                    UomId: "8ddcf8e9-7c89-4253-879a-16b52e598f65"
                };
                OrderLinesFactory.save(orderLine).then(function (answer) {
                    console.log("OrderLine sended");
                }, onError);
            }
        };

        function onError(answer) {
            alert("Error. Status: " + answer.status + "; StatusText: " + answer.statusText);
        };

    }

    return {
        restrict: "E",
        templateUrl: "templates/products/products.list-template.html",
        controller: controller
    };
});