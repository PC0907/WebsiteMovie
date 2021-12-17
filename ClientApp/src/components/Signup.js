import React, { Component } from "react";
import "./Login.css";

export class Signup extends Component {
  static displayName = Signup.name;

  constructor(props) {
    super(props);
    this.state = {Invalid: 0, loading: true };
  }
  /*let history = useHistory();
const handleRoute = () => {
    history.push("/maincourse");
}*/
  componentDidMount() {
    this.populateData();
  }

  static renderDiv(Invalid) {
    return (
      <>
        <div className='container1'>
          <div className='d-flex justify-content-center h-100'>
            <div className='card'>
              <div className='card-header'>
                <h3>Signup</h3>
                 {Invalid == "1" ? <small>The account already exists</small> : ""}
                <div className='d-flex justify-content-end social_icon'>
                  <span>
                    <i className='fab fa-facebook-square'></i>
                  </span>
                  <span>
                    <i className='fab fa-google-plus-square'></i>
                  </span>
                  <span>
                    <i className='fab fa-twitter-square'></i>
                  </span>
                </div>
              </div>
              <div className='card-body'>
                <form action='User/signup' method='post'>
                  <div className='input-group form-group'>
                    <div className='input-group-prepend'>
                      <span className='input-group-text'>
                        <i className='fas fa-user'></i>
                      </span>
                    </div>
                    <input
                      type='text'
                      name='username'
                      className='form-control'
                      placeholder='Username'
                    />
                  </div>
                  <div className='input-group form-group'>
                    <div className='input-group-prepend'>
                      <span className='input-group-text'>
                        <i className='fas fa-envelope'></i>
                      </span>
                    </div>
                    <input
                      type='text'
                      className='form-control'
                      placeholder='Email'
                      name='mail'
                    />
                  </div>
                  <div className='input-group form-group'>
                    <div className='input-group-prepend'>
                      <span className='input-group-text'>
                        <i class='fas fa-key'></i>
                      </span>
                    </div>
                    <input
                      type='password'
                      className='form-control'
                      placeholder='Password'
                      name='hashpassword'
                    />
                  </div>
                  <div className='row align-items-center remember' />

                  <div className='form-group'>
                    <input
                      type='submit'
                      value='Signup'
                      className='btn float-right login_btn'
                    />
                  </div>
                </form>
              </div>
            </div>
          </div>
        </div>
      </>
    );
  }

  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading..</em>
      </p>
    ) : (
      Signup.renderDiv(this.state.Invalid)
    );

    return <div>{contents}</div>;
  }

  async populateData() {
    const urlSearchParams = new URLSearchParams(window.location.search);
    const params = Object.fromEntries(urlSearchParams.entries());
    const invalid = params.invalid;

    this.setState({ Invalid: invalid, loading: false });
  }
}
