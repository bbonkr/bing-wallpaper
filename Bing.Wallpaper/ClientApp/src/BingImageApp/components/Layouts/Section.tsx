import React, { PropsWithChildren } from 'react';
import { ColorStyles, SizeStyles } from '../../models';
import { UiHelper } from '../../lib/UiHelper';

type SectionSize = 'medium' | 'large';

interface SectionProps {
    title?: React.ReactNode;
    subtitle?: React.ReactNode;
    size?: SectionSize;
    useHero?: boolean;
    heroColor?: ColorStyles;
    heroSize?: SizeStyles;
    classNames?: string[];
}

export const Section = ({
    title,
    subtitle,
    size,
    useHero,
    heroColor,
    heroSize,
    classNames,
    children,
}: PropsWithChildren<SectionProps>) => {
    return (
        <section
            className={`section ${
                size === 'medium'
                    ? 'is-medium'
                    : size === 'large'
                    ? 'is-large'
                    : ''
            } ${useHero ? 'hero' : ''} ${heroColor ?? ''} ${
                heroSize ?? ''
            } ${UiHelper.getClassNames(...(classNames ?? []))}`}
        >
            {useHero ? (
                <div className="hero-body">
                    <p className="title">{title}</p>
                    <p className="subtitle">{subtitle}</p>
                </div>
            ) : (
                <React.Fragment>
                    {title && <h1 className="title">{title}</h1>}
                    {subtitle && <h2 className="subtitle">{subtitle}</h2>}
                </React.Fragment>
            )}

            {children}
        </section>
    );
};
