var app = angular.module("anaApp", ['ngRoute','angularUtils.directives.dirPagination']);
app.config(['$locationProvider', function ($locationProvider) {
    $locationProvider.hashPrefix(''); //linke # yerine !# koymak için
}]);
app.config(function ($routeProvider) {
    $routeProvider.when('/Categories', {
        templateUrl: '/Templates/Categories/Index.html',
        controller:'categoriesController'
    });
    $routeProvider.when('/Products', {
        templateUrl: '/Templates/Products/Index.html',
        controller: 'productsController'
    });
    $routeProvider.when('/Suppliers', {
        templateUrl: '/Templates/Suppliers/Index.html',
        controller: 'suppliersController'
    });
    $routeProvider.otherwise({
        redirectTo:'/'
    });
});