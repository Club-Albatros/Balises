/** @jsx React.DOM */
var StatusButton = React.createClass({

  getInitialState: function() {
    this.resources = AlbatrosBalises.modules[this.props.moduleId].resources;
    this.service = AlbatrosBalises.modules[this.props.moduleId].service;
    this.security = AlbatrosBalises.modules[this.props.moduleId].security;
    return {
      currentStatus: this.props.status
    }
  },

  render: function() {
    var label = null;
    if (this.props.type == 'buttonandtext' | this.props.type == 'text') {
      label = <span>{this.resources['Status' + this.state.currentStatus]}</span>
    }
    var buttons = [];
    if (this.props.type == 'buttonandtext' | this.props.type == 'button') {
      if (this.security.IsVerifier) {
        switch (this.state.currentStatus)
        {
          case 1:
            buttons.push(<a href="#" onClick={this.changeStatus.bind(null, 2)} className="btn btn-xs btn-danger"
                            title={this.resources.Reject}>
                           <span className="glyphicon glyphicon-thumbs-down"></span> {this.resources.Reject}
                         </a>);
            buttons.push(<a href="#" onClick={this.changeStatus.bind(null, 4)} className="btn btn-xs btn-success"
                            title={this.resources.Accept}>
                           <span className="glyphicon glyphicon-thumbs-up"></span> {this.resources.Accept}
                         </a>);
            break;
          case 2:
            buttons.push(<a href="#" onClick={this.changeStatus.bind(null, 4)} className="btn btn-xs btn-success"
                            title={this.resources.Accept}>
                           <span className="glyphicon glyphicon-thumbs-up"></span> {this.resources.Accept}
                         </a>);
            break;
          case 4:
            buttons.push(<a href="#" onClick={this.changeStatus.bind(null, 2)} className="btn btn-xs btn-danger"
                            title={this.resources.Reject}>
                           <span className="glyphicon glyphicon-thumbs-down"></span> {this.resources.Reject}
                         </a>);
            break;
        }
      }
      if (this.security.IsPilot & this.security.UserId == this.props.pilotId) {
        switch (this.state.currentStatus)
        {
          case 0:
            buttons.push(<a href="#" onClick={this.changeStatus.bind(null, 1)} className="btn btn-xs btn-primary"
                            title={this.resources.Submit}>
                           <span className="glyphicon glyphicon-check"></span> {this.resources.Submit}
                         </a>);
            break;
          case 1:
            buttons.push(<a href="#" onClick={this.changeStatus.bind(null, 0)} className="btn btn-xs btn-warning"
                            title={this.resources.Retract}>
                           <span className="glyphicon glyphicon-remove"></span> {this.resources.Retract}
                         </a>);
            break;
        }
      }
    }
    return (
      <span className="statusbuttons">
       {label}
       {buttons}
      </span>
    );
  },

  changeStatus: function(newStatus, e) {
    e.preventDefault();
    this.service.changeFlightStatus(this.props.flightId, newStatus, function(data) {
      this.setState({
        currentStatus: data
      });
    }.bind(this));
  }

});

module.exports = StatusButton;