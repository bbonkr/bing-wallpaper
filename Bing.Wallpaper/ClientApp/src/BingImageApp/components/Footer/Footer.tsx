import React from 'react';
import { Link } from 'react-router-dom';
import { AppOptions } from '../../constants';

export const Footer = () => {
    return (
        <nav className="navbar navbar-fixed-bottom">
            <div className="navbar-content">
                <Link to="/" className="navbar-brand ml-auto">
                    <img src="/bbon-icon-48.png" alt="bbon icon" />
                </Link>
            </div>
            <span className="navbar-text ml-auto">
                &copy; {AppOptions.Title}
            </span>
        </nav>
    );
};
