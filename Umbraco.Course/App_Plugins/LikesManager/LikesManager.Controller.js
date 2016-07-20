angular.module("umbraco").controller("Level2.LikesManagerController",

    function ($scope, $http, entityResource, editorState, likesService) {

        var currentNodeId = editorState.current.id;
        console.log("Current node Id " + currentNodeId);

        $scope.load = function () {
            likesService.GetAll(currentNodeId)
                .then(
                    function(response) {
                        var likes = response.data;
                        angular.forEach(likes,
                            function(item) {
                                entityResource.getById(item.ChildId, "Member")
                                    .then(function(ent) {
                                        item.member = ent;
                                    });
                            });
                        $scope.likes = likes;
                        $scope.model.value = likes.length;
                    }
                );
        };

        $scope.delete = function(like) {
             likesService.delete(like.Id)
                .then(function() {
                    $scope.load();
                });
        };

        $scope.load();
    }
);