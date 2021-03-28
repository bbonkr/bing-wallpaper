import React from 'react';
import { Link } from 'react-router-dom';
import { AppOptions } from '../../constants';

export const Footer = () => {
    const handleClickScrollToTop = () => {
        const el = document.querySelector<HTMLDivElement>('.content-wrapper');
        if (el) {
            el.scrollTo({ top: 0, left: 0, behavior: 'smooth' });
        }
    };
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
            <div className="navbar-content">
                <button
                    className="btn"
                    title="Scroll to top of content"
                    onClick={handleClickScrollToTop}
                >
                    ⬆
                </button>
            </div>
        </nav>
    );
};
