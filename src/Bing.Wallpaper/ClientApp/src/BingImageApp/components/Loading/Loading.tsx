import React from 'react';
import { Section } from '../Layouts';

interface LoadingProps {
    message?: string;
}

export const Loading = ({ message }: LoadingProps) => {
    return (
        <Section classNames={['is-full-screen']}>
            <div className="is-flex-grow-1 is-flex is-flex-direction-column is-justify-content-center is-align-items-center">
                <h1 className="title">{message ?? 'Loading ...'}</h1>
            </div>
        </Section>
    );
};

export default Loading;
