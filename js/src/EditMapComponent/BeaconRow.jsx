module.exports = React.createClass({

  getInitialState: function() {
    this.resources = AlbatrosBalises.modules[this.props.moduleId].resources;
    this.service = AlbatrosBalises.modules[this.props.moduleId].service;
    return {
    }
  },

  render: function() {
    return (
        <tr data-beaconid={this.props.beacon.BeaconId}
            data-name={this.props.beacon.Name}
            data-id={this.props.beacon.Id}
            data-passorder={this.props.beacon.PassOrder}>
         <td>{this.props.beacon.Name}</td>
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