var BeaconRow = require('./BeaconRow.jsx');

var EditMap = React.createClass({

  getInitialState: function() {
    this.resources = AlbatrosBalises.modules[this.props.moduleId].resources;
    this.service = AlbatrosBalises.modules[this.props.moduleId].service;
    var initialBeaconList = this.props.track.map(function(b) {
      return {
        BeaconId: b.BeaconId,
        Name: b.Name,
        PassageTime: b.PassageTime.substr(11,5)
      }
    });
    initialBeaconList.sort(this.compareBeaconPassageTimes);
    return {
      passedBeacons: initialBeaconList
    }
  },

  render: function() {
    var beaconList = this.state.passedBeacons.map(function(elem) {
      return <BeaconRow beacon={elem} moduleId={this.props.moduleId}
                        changeBeaconTime={this.changeBeaconTime}
                        deleteBeacon={this.deleteBeacon} />;
    }.bind(this));
    var iconCol = {
      width: '40px'
    };
    var timeCol = {
      width: '120px'
    };
    return (
      <div className="row" ref="row">
       <div className="col-xs-12 col-md-8" ref="leftPane">
        <div ref="mapDiv"></div>
       </div>
       <div className="col-xs-12 col-md-4" ref="rightPane">
        <table className="table">
         <thead>
           <tr>
            <th>{this.resources.Beacon}</th>
            <th style={timeCol}>{this.resources.Time}</th>
            <th style={iconCol}></th>
           </tr>
         </thead>
         <tbody>
          {beaconList}
         </tbody>
        </table>
        <input type="hidden" id="BeaconList" name="BeaconList" value={JSON.stringify(this.state.passedBeacons)} />
       </div>
      </div>
    );
  },

  componentDidMount: function() {
    var googleScript = 'http://maps.googleapis.com/maps/api/js';
    if (this.props.apiKey && this.props.apiKey !== '') {
      googleScript += '&key=' + apiKey;
    }
    window.loadScript(googleScript, function() {
      this.expandDivToParent($(this.refs.row.getDOMNode()));
      this.expandDivToParent($(this.refs.leftPane.getDOMNode()));
      this.expandDivToParent($(this.refs.mapDiv.getDOMNode()));
      this._map = new google.maps.Map(this.refs.mapDiv.getDOMNode(), {
        scrollwheel: false,
        mapTypeId: google.maps.MapTypeId.TERRAIN
      });
      this._map.fitBounds(this.getBounds());
      for (var i=0;i<this.props.beacons.length;i++)
      {
        this.addPointToMap(this.props.beacons[i]);
      }
    }.bind(this));
  },

  expandDivToParent: function(div) {
    div.height(div.parent().height());
  },

  addPointToMap: function(point) {
    var marker = new google.maps.Marker({
      position: new google.maps.LatLng(point.Latitude, point.Longitude),
      map: this._map,
      label: point.Code,
      title: point.Code + ' ' + point.Name + ' (' + point.Points + ' pts)',
      beaconId: point.BeaconId,
      name: point.Name
    });
    marker.addListener('click', function(e) {
      this.addBeaconToList(marker);
    }.bind(this));
  },

  addBeaconToList: function(b) {
    var beacon = {
      BeaconId: b.beaconId,
      Name: b.name,
      PassageTime: '0:00'
    };
    console.log(this.state.passedBeacons);
    var newBeaconList = this.state.passedBeacons;
    newBeaconList.push(beacon);
    newBeaconList.sort(this.compareBeaconPassageTimes);
    this.setState({
      passedBeacons: newBeaconList
     });
  },

  getBounds: function() {
    var minLat = this.props.beacons[0].Latitude, 
        maxLat = this.props.beacons[0].Latitude, 
        minLong = this.props.beacons[0].Longitude, 
        maxLong = this.props.beacons[0].Longitude;
    for (var i=1;i<this.props.beacons.length;i++)
    {
      if(this.props.beacons[i].Latitude < minLat) {
        minLat = this.props.beacons[i].Latitude;
      }
      if(this.props.beacons[i].Latitude > maxLat) {
        maxLat = this.props.beacons[i].Latitude;
      }
      if(this.props.beacons[i].Longitude < minLong) {
        minLong = this.props.beacons[i].Longitude;
      }
      if(this.props.beacons[i].Longitude > maxLong) {
        maxLong = this.props.beacons[i].Longitude;
      }
    }
    return new google.maps.LatLngBounds({
      lat: minLat,
      lng: minLong
    }, {
      lat: maxLat,
      lng: maxLong
    });
  },

  changeBeaconTime: function(beacon, e) {
    beacon.PassageTime = e.target.value;
    var newBeaconList = this.state.passedBeacons.map(function(item) {
      if (item.BeaconId == beacon.BeaconId) {
        return beacon;
      } else {
        return item;
      }
    });
    newBeaconList.sort(this.compareBeaconPassageTimes);
    this.setState({
      passedBeacons: newBeaconList
     });
  },

  deleteBeacon: function(beacon, e) {
    e.preventDefault();
    var newBeaconList = [];
    for (i=0;i<this.state.passedBeacons.length;i++) {
      if (this.state.passedBeacons[i].BeaconId != beacon.BeaconId) {
        newBeaconList.push(this.state.passedBeacons[i]);
      }
    }
    newBeaconList.sort(this.compareBeaconPassageTimes);
    this.setState({
      passedBeacons: newBeaconList
     });
  },

  compareBeaconPassageTimes: function(a,b) {
    if (a.PassageTime < b.PassageTime)
      return -1;
    else if (a.PassageTime > b.PassageTime)
      return 1;
    else 
      return 0;
  }


});

module.exports = EditMap;