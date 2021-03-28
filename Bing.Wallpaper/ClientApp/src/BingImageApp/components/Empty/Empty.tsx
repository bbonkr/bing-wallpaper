import React from 'react';

interface EmptyProps {
    title?: React.ReactNode;
    content?: React.ReactNode;
}

export const Empty = ({ title, content }: EmptyProps) => {
    return (
        <div>
            <h2>{title}</h2>
            <div>{content}</div>
        </div>
    );
};
