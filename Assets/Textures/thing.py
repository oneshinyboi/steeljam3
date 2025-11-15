from PIL import Image

def make_color_transparent(image_path, output_path, color_to_make_transparent):
    """
    Loads an image and makes all pixels of a specified color transparent.

    Args:
        image_path (str): The path to the input image.
        output_path (str): The path to save the new image with transparency.
        color_to_make_transparent (tuple): An RGB tuple (r, g, b) of the color to make transparent.
    """
    try:
        # Open the image
        img = Image.open(image_path)
    except FileNotFoundError:
        print(f"Error: The file '{image_path}' was not found.")
        return

    # Convert the image to RGBA mode to support transparency
    img = img.convert("RGBA")

    # Get the image data
    datas = img.getdata()

    new_data = []
    for item in datas:
        # Check if the pixel color matches the target color
        # item[0] is Red, item[1] is Green, item[2] is Blue
        if item[0] == color_to_make_transparent[0] and item[1] == color_to_make_transparent[1] and item[2] == color_to_make_transparent[2]:
            # If it matches, make it transparent (Alpha channel set to 0)
            new_data.append((255, 255, 255, 0))
        else:
            # Otherwise, keep the original pixel
            new_data.append(item)

    # Update image data
    img.putdata(new_data)

    # Save the new image, must be a format that supports transparency like PNG
    img.save(output_path, "PNG")
    print(f"Successfully processed '{image_path}' and saved the output to '{output_path}'")


# --- Main execution ---
if __name__ == "__main__":
    # Define the input and output file paths
    input_image = 'clouds.png'
    output_image = 'clouds_transparent.png'

    # The RGB color of the blue background from your image is (0, 162, 232).
    # You can change this to any other RGB color you want to make transparent.
    # For example, to make pure red transparent, you would use (255, 0, 0).
    background_color = (78, 173, 245)

    # Run the function
    make_color_transparent(input_image, output_image, background_color)