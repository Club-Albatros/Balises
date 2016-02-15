var Upload = require('./UploadComponent/Upload.jsx'),
    ViewMap = require('./ViewMapComponent/ViewMap.jsx'),
    EditMap = require('./EditMapComponent/EditMap.jsx');

(function($, window, document, undefined) {

  $(document).ready(function() {
    AlbatrosBalises.loadData();
  });

  window.AlbatrosBalises = {
    modules: {},
    googleLoaded: false,

    loadData: function() {
      $('.albatrosBalises').each(function(i, el) {
        var moduleId = $(el).data('moduleid');
        var resources = $(el).data('resources');
        var security = $(el).data('security');
        AlbatrosBalises.modules[moduleId] = {
          service: new AlbatrosBalisesService($, moduleId),
          resources: resources,
          security: security
        };
      });
      $('.uploadComponent').each(function(i, el) {
        var moduleId = $(el).data('moduleid');
        var tabId = $(el).data('tabid');
        React.render(<Upload moduleId={moduleId} tabId={tabId} />, el);
      });
      $('.viewMap').each(function(i, el) {
        var moduleId = $(el).data('moduleid');
        var apiKey = $(el).data('apikey');
        var track = $(el).data('track');
        var flight = $(el).data('flight');
        React.render(<ViewMap apiKey={apiKey} moduleId={moduleId} track={track}
                 flight={flight} />, el);
      });
      $('.editMap').each(function(i, el) {
        var moduleId = $(el).data('moduleid');
        var apiKey = $(el).data('apikey');
        var track = $(el).data('track');
        var flight = $(el).data('flight');
        var beacons = $(el).data('beacons');
        React.render(<EditMap apiKey={apiKey} moduleId={moduleId} track={track}
                 flight={flight} beacons={beacons} />, el);
      });
    }

  }


})(jQuery, window, document);
