import React from 'react';
import { useImagesApi } from '../../hooks/useImagesApi';
import { ImageItemModel } from '../../models';
import './style.css';

interface ImageCardProps {
    image: ImageItemModel;
}

export const ImageCard = ({ image }: ImageCardProps) => {
    const { showFullSizeImage } = useImagesApi();

    const handleClickImage = () => {
        showFullSizeImage(image);
    };
    return (
        <div
            className="card p-0 border-0 w-350 position-relative clickable"
            onClick={handleClickImage}
        >
            <img
                src={`/api/v1.0/files/${encodeURIComponent(
                    `${image.fileName}`,
                )}`}
                className="img-fluid rounded-top rounded-bottom"
                alt={image.fileName}
            />
            <div className="card-title-over-image rounded-bottom">
                <p>{image.fileName}</p>
            </div>
        </div>
    );
};
