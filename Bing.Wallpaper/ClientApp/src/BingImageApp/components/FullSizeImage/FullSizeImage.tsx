import React, { useEffect, useRef } from 'react';
import { useImagesApi } from '../../hooks/useImagesApi';
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
                            className="btn"
                            title="Full screen"
                            onClick={handleClickFullScreen}
                        >
                            []
                        </button>
                    </div>
                    <div>
                        <p>{fullSizeImage.fileName}</p>
                    </div>
                    <div>
                        <button
                            className="btn"
                            title="Close"
                            onClick={handleClickClose}
                        >
                            &times;
                        </button>
                    </div>
                </div>
                <img
                    ref={imageRef}
                    src={`/api/v1.0/files/${encodeURIComponent(
                        `${fullSizeImage.fileName}`,
                    )}`}
                    alt={fullSizeImage?.fileName}
                />
            </div>
        )
    );
};
