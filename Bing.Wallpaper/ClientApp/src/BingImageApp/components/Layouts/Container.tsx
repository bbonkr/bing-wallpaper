import React, { PropsWithChildren } from 'react';
import { UiHelper } from '../../lib/UiHelper';

interface ContainerProps {
    classNames?: string[];
}

export const Container = ({
    classNames,
    children,
}: PropsWithChildren<ContainerProps>) => {
    return (
        <div
            className={`container ${UiHelper.getClassNames(
                ...(classNames ?? []),
            )}`}
        >
            {children}
        </div>
    );
};
