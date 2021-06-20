import React, { useEffect, useRef } from 'react';
import { useImagesApi } from '../../hooks/useImagesApi';
import { FaExpandArrowsAlt, FaTimes } from 'react-icons/fa';
import './style.css';

export const FullSizeImage = () => {
    const { fullSizeImage, hideFullSizeImage } = useImagesApi();
    const imageRef = useRef<HTMLImageElement>(null);

    const handleClickClose = () => {
        hideFullSizeImage();
    };

    const handleClickFullScreen = () => {
        imageRef.current?.requestFullscreen();
    };

    useEffect(() => {
        const handleKeyUp = (ev: KeyboardEvent) => {
            if (ev.key === 'Escape') {
                hideFullSizeImage();
            }
        };

        if (fullSizeImage) {
            window.addEventListener('keyup', handleKeyUp);
        } else {
            window.removeEventListener('keyup', handleKeyUp);
        }

        return () => {
            window.removeEventListener('keyup', handleKeyUp);
        };
    }, [fullSizeImage]);

    return (
        fullSizeImage && (
            <div className="full-size-image-container">
                <div className="tools">
                    <div>
                        <button
                            className="button"
                            title="Full screen"
                            onClick={handleClickFullScreen}
                        >
                            <FaExpandArrowsAlt />
                        </button>
                    </div>
                    <div>
                        <p>{fullSizeImage.title ?? fullSizeImage.fileName}</p>
                    </div>
                    <div>
                        <button
                            className="button"
                            title="Close"
                            onClick={handleClickClose}
                        >
                            <FaTimes />
                        </button>
                    </div>
                </div>
                <figure>
                    <img
                        ref={imageRef}
                        src={`/api/v1.0/files/${encodeURIComponent(
                            `${fullSizeImage.fileName}`,
                        )}`}
                        title={fullSizeImage.title}
                        alt={fullSizeImage?.fileName}
                    />
                </figure>
            </div>
        )
    );
};
