module.exports = React.createClass({

  getInitialState: function() {
    this.resources = AlbatrosBalises.modules[this.props.moduleId].resources;
    this.service = AlbatrosBalises.modules[this.props.moduleId].service;
    this.security = AlbatrosBalises.modules[this.props.moduleId].security;
    return {
    }
  },

  render: function() {
    if (this.security.IsPilot) {
      return (
        <a href="#" className="btn btn-default cmdUpload">{this.resources.Upload}</a>
      );
    } else {
      return null;
    }
  },

  componentDidMount: function() {
    $(document).ready(function() {
      var uploader = new ss.SimpleUpload({
        button: $('.cmdUpload'),
        url: this.service.ServicePath + 'Flights/Upload',
        customHeaders: {
          ModuleId: this.props.moduleId,
          TabId: this.props.tabId,
          RequestVerificationToken: $('[name="__RequestVerificationToken"]').val()
        },
        name: 'resource',
        multipart: true,
        hoverClass: 'hover',
        focusClass: 'focus',
        responseType: 'json',
        startXHR: function() {},
        onSubmit: function() {
          // this.refs.msgBox.getDOMNode().innerHtml = this.resources.Uploading;
        }.bind(this),
        onComplete: function(filename, response) {
          // this.refs.msgBox.getDOMNode().innerHtml = this.resources.DropHere;
          if (response.FlightId == undefined) {
            window.location = this.props.pilotUrl;
          } else {
            window.location = this.props.flightUrl.replace('-1', response.FlightId);
          }
        }.bind(this),
        onError: function(filename, errorType, status, statusText, response, uploadBtn, fileSize) {
          // this.refs.msgBox.getDOMNode().innerHtml = 'Error uploading file: ' + response;
        }.bind(this)
      });
    }.bind(this));
  }

});
