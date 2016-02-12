var Upload = React.createClass({

  getInitialState: function() {
    this.resources = AlbatrosBalises.modules[this.props.moduleId].resources;
    this.service = AlbatrosBalises.modules[this.props.moduleId].service;
    this.security = AlbatrosBalises.modules[this.props.moduleId].security;
    return {
    }
  },

  render: function() {
    var uploadPanel = null;
    if (this.security.IsPilot) {
      uploadPanel = (
        <div className="dropPanel">
          <a href="#" className="btn btn-default cmdUpload">{this.resources.Upload}</a>
          <div ref="msgBox">{this.resources.DropHere}</div>
        </div>
        );  
    }
    return (
      <div className="Upload">{uploadPanel}</div>
    );
  },

  componentDidMount: function() {
    $(document).ready(function() {
      var uploader = new ss.SimpleUpload({
        button: $('.cmdUpload'),
        dropzone: $('.dropPanel'),
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
          this.refs.msgBox.getDOMNode().innerHtml = this.resources.Uploading;
        }.bind(this),
        onComplete: function(filename, response) {
          this.refs.msgBox.getDOMNode().innerHtml = this.resources.DropHere;
          location.reload();
        }.bind(this),
        onError: function(filename, errorType, status, statusText, response, uploadBtn, fileSize) {
          this.refs.msgBox.getDOMNode().innerHtml = 'Error uploading file: ' + response;
        }.bind(this)
      });
    }.bind(this));
  }

});

module.exports = Upload;