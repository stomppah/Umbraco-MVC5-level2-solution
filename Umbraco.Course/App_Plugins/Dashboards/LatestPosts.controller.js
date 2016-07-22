angular.module("umbraco")
    .controller("level2.latestsPostsController",
    function ($scope, $http, latestsPostsService) {
        latestsPostsService.getAll()
            .success(function(data) {
                $scope.posts = data;
            });
    });
