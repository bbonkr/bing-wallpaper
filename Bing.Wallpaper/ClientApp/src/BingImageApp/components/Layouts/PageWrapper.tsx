import React, { PropsWithChildren } from 'react';

interface PageWrapperProsp {
    withNavbar?: boolean;
    withSidebar?: boolean;
    withNavbarFixedBottom?: boolean;
}

export const PageWrapper = ({
    children,
    withNavbar,
    withSidebar,
    withNavbarFixedBottom,
}: PropsWithChildren<PageWrapperProsp>) => {
    return (
        <div
            className={`page-wrapper ${withNavbar ? 'with-navbar' : ''} ${
                withSidebar ? 'with-sidebar' : ''
            } ${withNavbarFixedBottom ? 'with-navbar-fixed-bottom' : ''}`}
        >
            {children}
        </div>
    );
};
