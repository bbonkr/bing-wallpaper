import React from 'react';
import { FaArrowUp } from 'react-icons/fa';

interface ScrollToTopProps {
    show: boolean;
    containerClassName?: string;
    containerStyle?: React.CSSProperties;
    buttonClassName?: string;
    buttonStyle?: React.CSSProperties;
    buttonContent?: React.ReactNode;
    onClick?: () => void;
}

export const ScrollToTop = ({
    show,
    containerClassName,
    containerStyle,
    buttonClassName,
    buttonStyle,
    buttonContent,
    onClick,
}: ScrollToTopProps) => {
    if (!show) {
        return <React.Fragment></React.Fragment>;
    }

    return (
        <div
            className={`${containerClassName ?? ''}`}
            style={{
                ...(containerStyle ?? {}),
            }}
        >
            <button
                className={` ${buttonClassName ?? 'button'}`}
                style={{ ...(buttonStyle ?? {}) }}
                onClick={onClick}
            >
                {buttonContent ?? <FaArrowUp title="위로" />}
            </button>
        </div>
    );
};
