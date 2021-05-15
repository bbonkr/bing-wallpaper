import React, { useEffect, useState } from 'react';
import { LinkModel } from '../../models';
import { GenericLink } from '../GenericLink';
import { AppOptions } from '../../constants';
import { FaGithub } from 'react-icons/fa';
interface HeaderProps {
    menuRoutes: LinkModel[];
}

export const Header = ({ menuRoutes }: HeaderProps) => {
    const [navbarMenuIsActive, setNavbarMenuIsActive] = useState(false);
    const handleClickMenu = () => {
        window.scrollTo({ top: 0, left: 0, behavior: 'smooth' });
        setNavbarMenuIsActive((prevState) => false);
    };

    const handleToggleMenu = (
        event: React.MouseEvent<HTMLAnchorElement, MouseEvent>,
    ) => {
        event.preventDefault();
        setNavbarMenuIsActive((prevState) => !prevState);
    };

    const handleClickOuter = () => {
        setNavbarMenuIsActive((_) => false);
    };

    const handleWindowResize = () => {
        const headerNavElement = document.querySelector<HTMLDivElement>(
            '#header-nav',
        );
        if (headerNavElement) {
            const styles = window.getComputedStyle(headerNavElement);
            const displayValue = styles.getPropertyValue('display');

            if (displayValue === 'flex') {
                setNavbarMenuIsActive((_) => false);
            }
        }
    };

    useEffect(() => {
        // let observer: MutationObserver;

        // const headerNavElement = document.querySelector<HTMLDivElement>('#header-nav');
        // if (headerNavElement) {
        //     const config: MutationObserverInit = {
        //         attributes: true,
        //         childList: true,
        //         characterData: true,
        //     };
        //     const callback = (mutations: MutationRecord[], observer: MutationObserver) => {
        //         console.info(observer);
        //         for (const mutation of mutations) {
        //             if (mutation.type === 'childList') {
        //                 console.info('observer childList');
        //             }
        //             if (mutation.type === 'attributes') {
        //                 console.info('observer attributeName', mutation.attributeName);
        //             }
        //         }
        //     };
        //     observer = new MutationObserver(callback);
        //     observer.observe(headerNavElement, config);
        // }

        window.addEventListener('resize', handleWindowResize);
        handleWindowResize();

        return () => {
            // if (observer) {
            //     observer.disconnect();
            // }
            window.removeEventListener('resize', handleWindowResize);
        };
    }, []);

    return (
        <>
            {navbarMenuIsActive && (
                <div
                    style={{
                        position: 'fixed',
                        top: 0,
                        left: 0,
                        right: 0,
                        bottom: 0,
                        background: '#000000',
                        opacity: 0.7,
                        zIndex: 1000,
                    }}
                    onClick={handleClickOuter}
                ></div>
            )}
            <nav
                className="navbar is-fixed-top has-dropdown"
                id="header-nav"
                role="navigation"
                aria-label="main navigation"
                style={{ zIndex: 1001 }}
            >
                <div className="navbar-brand">
                    <GenericLink
                        record={{ href: '/', title: '' }}
                        classNames={['navbar-item']}
                        onClick={handleClickMenu}
                    >
                        <img src="/bbon-icon-48.png" width="auto" height="28" />{' '}
                        <span className="ml-3">{AppOptions.Title}</span>
                    </GenericLink>
                    <a
                        role="button"
                        className={`navbar-burger ${
                            navbarMenuIsActive ? 'is-active' : ''
                        }`}
                        aria-label="menu"
                        aria-expanded="false"
                        data-target="navbarMainHeader"
                        onClick={handleToggleMenu}
                    >
                        <span aria-hidden="true"></span>
                        <span aria-hidden="true"></span>
                        <span aria-hidden="true"></span>
                    </a>
                </div>

                <div
                    id="navbarMainHeader"
                    className={`navbar-menu ${
                        navbarMenuIsActive ? 'is-active' : ''
                    }`}
                    onBlurCapture={() => setNavbarMenuIsActive((_) => false)}
                >
                    <div className="navbar-start">
                        {menuRoutes.map((menu) => {
                            return (
                                <GenericLink
                                    classNames={['navbar-item']}
                                    record={{
                                        href: menu.href,
                                        title: menu.title,
                                    }}
                                    key={menu.href}
                                    onClick={handleClickMenu}
                                />
                            );
                        })}
                    </div>

                    <div className="navbar-end">
                        <div className="navbar-item">
                            <div className="buttons">
                                <a
                                    className="button"
                                    href="https://github.com/bbonkr/bing-wallpaper"
                                    target="_blank"
                                    rel="external"
                                    title="Navigate to github bing-wallpaper page"
                                >
                                    <FaGithub />
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </nav>
        </>
    );
};
