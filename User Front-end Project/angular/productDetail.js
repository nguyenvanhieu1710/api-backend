var app = angular.module("MyProject", []);

app.controller("ProductDetailCtrl", function ($scope, $http) {
  $scope.product;

  var url = window.location.href;
  var params = new URLSearchParams(window.location.search); // Lấy phần query string sau dấu ?
  var productId = params.get("productId"); // Lấy giá trị của tham số productId
  // debugger;
  $scope.loadProduct = function () {
    // debugger;
    $http({
      method: "GET",
      url: current_url + "/api-user/product/get-data-by-id/" + productId,
    })
      .then(function (response) {
        $scope.product = response.data;
        // makeScript("js/main.js");
        // debugger;
      })
      .catch(function (error) {
        console.log("Request failed: " + error.data);
      });
  };

  $scope.loadProduct();
});
