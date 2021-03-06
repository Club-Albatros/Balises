var Comment = React.createClass({

  getInitialState: function() {
    this.resources = AlbatrosBalises.modules[this.props.moduleId].resources;
    this.security = AlbatrosBalises.modules[this.props.moduleId].security;
    return {
    }
  },

  render: function() {
    var imgUrl = this.props.appPath + '/DnnImageHandler.ashx?mode=profilepic&w=64&h=64&userId=' + this.props.comment.UserId;
    var actionBar = null;
    if (this.security.IsAdmin | this.security.UserId == this.props.comment.UserId) {
      actionBar = (
                  <div className="action">
                      <button type="button" className="btn btn-danger btn-xs" 
                              title={this.resources.Delete} onClick={this.props.onDelete.bind(null, this.props.comment.CommentId)}
                              data-id={this.props.comment.CommentId}>
                          <span className="fa fa-trash"></span>
                      </button>
                  </div>
        );
    }
    return (
      <li className="list-group-item">
          <div className="row">
              <div className="img-col">
                  <img src={imgUrl} className="img-circle img-responsive" alt="" /></div>
              <div className="comment-col">
                  <div className="comment-details"></div>
                  <div className="comment-text">{this.props.comment.Remarks}</div>
                  {actionBar}
              </div>
          </div>
      </li>
    );
  }

});

module.exports = Comment;