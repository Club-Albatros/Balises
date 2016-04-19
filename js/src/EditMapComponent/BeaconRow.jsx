module.exports = React.createClass({

  getInitialState: function() {
    this.resources = AlbatrosBalises.modules[this.props.moduleId].resources;
    this.service = AlbatrosBalises.modules[this.props.moduleId].service;
    return {
    }
  },

  render: function() {
    var isFirst = (this.props.beacon.PassOrder==1);
    var isLast = (this.props.beacon.PassOrder==this.props.total);
    var upButton = null;
    if (!isFirst) {
      upButton = (
          <a href="#" className="btn btn-sm btn-info" title={this.resources.Remove}
             onClick={this.props.moveBeacon.bind(null, this.props.beacon, -1)}>
           <span className="fa fa-arrow-up"></span>
          </a>
        );
    }
    var downButton = null;
    if (!isLast) {
      downButton = (
          <a href="#" className="btn btn-sm btn-info" title={this.resources.Remove}
             onClick={this.props.moveBeacon.bind(null, this.props.beacon, 1)}>
           <span className="fa fa-arrow-down"></span>
          </a>
        );
    }
    return (
        <tr data-beaconid={this.props.beacon.BeaconId}
            data-name={this.props.beacon.Name}
            data-id={this.props.beacon.Id}
            data-passorder={this.props.beacon.PassOrder}>
         <td>{this.props.beacon.Name}</td>
         <td>{upButton}</td>
         <td>{downButton}</td>
         <td>
          <a href="#" className="btn btn-sm btn-danger" title={this.resources.Remove}
             onClick={this.props.deleteBeacon.bind(null, this.props.beacon)}>
           <span className="fa fa-remove"></span>
          </a>
         </td>
        </tr>
    );
  }

});