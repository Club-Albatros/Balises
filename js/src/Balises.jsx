var Upload = require('./UploadComponent/Upload.jsx');

(function($, window, document, undefined) {

  $(document).ready(function() {
    AlbatrosBalises.loadData();
  });

  window.AlbatrosBalises = {
    modules: {},

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
    },

    formatString: function(format) {
      var args = Array.prototype.slice.call(arguments, 1);
      return format.replace(/{(\d+)}/g, function(match, number) {
        return typeof args[number] != 'undefined' ? args[number] : match;
      });
    }


  }


})(jQuery, window, document);
