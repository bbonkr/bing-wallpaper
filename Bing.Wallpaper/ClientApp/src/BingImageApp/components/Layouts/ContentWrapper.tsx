import React, { PropsWithChildren } from 'react';

interface ContentWarpperProps {}

export const ContentWrapper = ({
    children,
}: PropsWithChildren<ContentWarpperProps>) => {
    return <div className="content-wrapper">{children}</div>;
};
