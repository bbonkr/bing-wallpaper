import React from 'react';
import { Link } from 'react-router-dom';
import { AppOptions } from '../../constants';
export const Header = () => {
    return (
        <nav className="navbar">
            <Link to="/" className="navbar-brand">
                <img src="/bbon-icon-48.png" alt="bbon icon" />
                {AppOptions.Title}
            </Link>

            <span className="navbar-text text-monospace">v0.0.1-beta</span>

            <ul className="navbar-nav d-none d-md-flex">
                <li className="nav-item active">
                    <Link to="/" className="nav-link">
                        Images
                    </Link>
                </li>
                <li className="nav-item active">
                    <Link to="/logs" className="nav-link">
                        Logs
                    </Link>
                </li>
            </ul>
        </nav>
    );
};
