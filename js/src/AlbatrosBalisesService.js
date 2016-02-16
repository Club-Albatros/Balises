window.AlbatrosBalisesService = function($, mid) {
    var moduleId = mid;

    this.ServicePath = $.dnnSF(moduleId).getServiceRoot('Albatros/Balises');

    this.ajaxCall = function(type, controller, action, id, data, success, fail) {
        // showLoading();
        $.ajax({
            type: type,
            url: this.ServicePath + controller + '/' + action + (id != null ? '/' + id : ''),
            beforeSend: $.dnnSF(moduleId).setModuleHeaders,
            data: data
        }).done(function(retdata) {
            // hideLoading();
            if (success != undefined) {
                success(retdata);
            }
        }).fail(function(xhr, status) {
            // showError(xhr.responseText);
            if (fail != undefined) {
                fail(xhr.responseText);
            }
        });
    }

    this.getInitialData = function(success) {
        this.ajaxCall('GET', 'Module', 'InitialData', null, null, success);
    }

    this.updateSettings = function(newSettings, success) {
        this.ajaxCall('POST', 'Settings', 'Update', null, newSettings, success);
    }

    this.submitPoint = function(newFlight, success) {
        this.ajaxCall('POST', 'Flights', 'Flight', null, newFlight, success);
    }

    this.deletePoint = function(id, success) {
        this.ajaxCall('POST', 'Flights', 'Delete', id, null, success);
    }

    this.listTakeoffs = function(searchString, success) {
        this.ajaxCall('GET', 'Sites', 'Takeoffs', null, { SearchString: searchString }, success);
    }

    this.listLandings = function(searchString, success) {
        this.ajaxCall('GET', 'Sites', 'Landings', null, { SearchString: searchString }, success);
    }

    this.searchClosestTakeoff = function(coords, success) {
        this.ajaxCall('GET', 'Sites', 'ClosestTakeoff', null, { Coords: coords }, success);
    }

    this.searchClosestLanding = function(coords, success) {
        this.ajaxCall('GET', 'Sites', 'ClosestLanding', null, { Coords: coords }, success);
    }

    this.changeFlightStatus = function(flightId, newStatus, success) {
        this.ajaxCall('POST', 'Flights', 'ChangeStatus', flightId, { NewStatus: newStatus }, success);
    }

}
