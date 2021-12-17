import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true,
      auth: false
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  componentDidMount() {

    const checkAuth = async () => {
    const url = "User/checkAuth";
    const response = await fetch(url);
    const info = await response.json();
    console.log("Auth response is", info);
    this.setState({collapsed: true, auth: info})
  }
    checkAuth();
  }

  static renderDiv (auth, collapsed) {
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
          <Container>
            <NavbarBrand tag={Link} to="/">DotNetAssignment</NavbarBrand>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!collapsed} navbar>
              <ul className="navbar-nav flex-grow">
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                </NavItem>

                {auth ? 
                  <>
                  <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/profile">My profile</NavLink>
                </NavItem>
                <NavItem>
                  <a className="text-dark" href="/User/logout">Logout</a>
                </NavItem>
                </>
                 : <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/login">Login or Register</NavLink>
                </NavItem>}
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
  }

  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading..</em>
      </p>
    ) : (
      NavMenu.renderDiv(this.state.auth, this.state.collapsed)
    );

    return <div>{contents}</div>;
  }
}
