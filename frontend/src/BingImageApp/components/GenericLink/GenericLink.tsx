import React, { PropsWithChildren } from 'react';
import { FaExternalLinkAlt } from 'react-icons/fa';
import Link from 'next/link';
import { LinkModel } from '../../models';
import { UiHelper } from '../../lib/UiHelper';

interface GenericLinkProps {
    record: LinkModel;
    classNames?: string[];
    icon?: React.ReactNode;
    onClick?: (event?: React.MouseEvent<HTMLAnchorElement, MouseEvent>) => void;
}

export const GenericLink = ({
    record,
    classNames,
    icon,
    children,
    onClick,
}: PropsWithChildren<GenericLinkProps>) => {
    const handleClick = (
        event: React.MouseEvent<HTMLAnchorElement, MouseEvent>,
    ) => {
        if (onClick) {
            onClick(event);
        }
    };

    return record.href.startsWith('/') ? (
        <Link
            href={record.href}
            className={UiHelper.getClassNames(...(classNames ?? []))}
            onClick={handleClick}
        >
            {children ?? record.title}
        </Link>
    ) : (
        <a
            className={`${UiHelper.getClassNames(...(classNames ?? []))}`}
            href={record.href}
            target={record.target || '_blank'}
            rel={
                (record.target || '_blank') === '_blank'
                    ? 'noreferrer noopener'
                    : ''
            }
            onClick={handleClick}
        >
            {children ?? (
                <React.Fragment>
                    {icon && icon}
                    <span className="mr-1">{record.title}</span>
                    {(!record.target || record.target !== '_self') && (
                        <span>
                            <FaExternalLinkAlt />
                        </span>
                    )}
                </React.Fragment>
            )}
        </a>
    );
};
