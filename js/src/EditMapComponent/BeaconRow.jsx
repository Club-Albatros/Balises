var BeaconRow = React.createClass({

  getInitialState: function() {
    this.resources = AlbatrosBalises.modules[this.props.moduleId].resources;
    this.service = AlbatrosBalises.modules[this.props.moduleId].service;
    return {
    }
  },

  render: function() {
    return (
        <tr>
         <td>{this.props.beacon.Name}</td>
         <td>
           <div className="input-group" ref="time">
             <input type="text" className="form-control" value={this.props.beacon.PassageTime} 
                    ref="txtTime" />
             <span className="input-group-addon clickable">
              <span className="glyphicon glyphicon-time"></span>
             </span>
           </div>
         </td>
         <td>
          <a href="#" className="btn btn-sm btn-danger" title={this.resources.Remove}
             onClick={this.props.deleteBeacon.bind(null, this.props.beacon)}>
           <span className="glyphicon glyphicon-remove"></span>
          </a>
         </td>
        </tr>
    );
  },

  componentDidMount: function() {
    $(document).ready(function () {
      console.log('hooking up');
      $(this.refs.time.getDOMNode()).clockpicker({
        autoclose: true
      });
      $(this.refs.txtTime.getDOMNode()).on('change', function(e) {
        this.props.changeBeaconTime(this.props.beacon, e);
      }.bind(this));
    }.bind(this));
  }

});

module.exports = BeaconRow;