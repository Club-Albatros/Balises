var Upload = require('./UploadComponent/Upload.jsx'),
    UploadButton = require('./UploadComponent/UploadButton.jsx'),
    ViewMap = require('./ViewMapComponent/ViewMap.jsx'),
    EditMap = require('./EditMapComponent/EditMap.jsx'),
    StatusButton = require('./StatusButton.jsx'),
    Comments = require('./CommentsComponent/Comments.jsx');

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
      $('.uploadButton').each(function(i, el) {
        var moduleId = $(el).data('moduleid');
        var tabId = $(el).data('tabid');
        React.render(<UploadButton moduleId={moduleId} tabId={tabId}
                                   flightUrl={$(el).data('flighturl')}
                                   pilotUrl={$(el).data('piloturl')} />, el);
      });
      $('.viewMap').each(function(i, el) {
        var moduleId = $(el).data('moduleid');
        var apiKey = $(el).data('apikey');
        var track = $(el).data('track');
        var flight = $(el).data('flight');
        React.render(<ViewMap apiKey={apiKey} moduleId={moduleId} track={track}
                 flight={flight}
                 scheme={$(el).data('scheme')} />, el);
      });
      $('.editMap').each(function(i, el) {
        var moduleId = $(el).data('moduleid');
        var apiKey = $(el).data('apikey');
        var track = $(el).data('track');
        var flight = $(el).data('flight');
        var beacons = $(el).data('beacons');
        React.render(<EditMap apiKey={apiKey} moduleId={moduleId} track={track}
                 flight={flight} beacons={beacons}
                 scheme={$(el).data('scheme')} />, el);
      });
      $('.btnStatus').each(function(i, el) {
        var moduleId = $(el).data('moduleid');
        var status = $(el).data('status');
        var flightId = $(el).data('flightid');
        var pilotId = $(el).data('pilotid');
        var type = $(el).data('type');
        React.render(<StatusButton moduleId={moduleId} status={status} flightId={flightId} pilotId={pilotId} type={type} />, el);
      });
      $('.commentsComponent').each(function(i, el) {
        var moduleId = $(el).data('moduleid');
        var flightId = $(el).data('flight');
        var pageSize = $(el).data('pagesize');
        var comments = $(el).data('comments');
        var appPath = $(el).data('apppath');
        var totalComments = $(el).data('totalcomments');
        var loggedIn = $(el).data('loggedin');
        React.render(<Comments moduleId={moduleId} 
           appPath={appPath} flightId={flightId}
           totalComments={totalComments} loggedIn={loggedIn}
           pageSize={pageSize} comments={comments} />, el);
      });
    }

  }


})(jQuery, window, document);
