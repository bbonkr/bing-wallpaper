import React from 'react';
import { Navigate, useLocation } from 'react-router-dom';

interface RedirectProps {
    to: string;
    replace?: boolean;
}

export const Redirect = ({ to, replace }: RedirectProps) => {
    const location = useLocation();

    return <Navigate replace={replace} to={to} state={{ from: location }} />;
};
