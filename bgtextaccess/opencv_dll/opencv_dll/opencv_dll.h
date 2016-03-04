
#include <opencv2/core/core.hpp>
#include <opencv2/imgproc/imgproc.hpp>
#include <opencv2/highgui/highgui.hpp>

#define OPENCV_IMGPROC_EXPORT

#ifdef OPENCV_IMGPROC_EXPORT
#define OPENCV_IMGPROC_API __declspec(dllexport) 
#else
#define OPENCV_IMGPROC_API __declspec(dllimport) 
#endif




extern "C" {

	class opencv_wrapper{
	private :
		cv::Mat mx1, my1, mx2, my2;
		//rectify maps in CV32F/float
	public:
		cv::Mat mx1_f, my1_f, mx2_f, my2_f;
		opencv_wrapper();
		~opencv_wrapper();
		bool read_calib();
	};

	OPENCV_IMGPROC_API opencv_wrapper* _cdecl create_opencv_wrapper();
	OPENCV_IMGPROC_API void delete_opencv_wrapper(opencv_wrapper* opencv_wrapper_ptr);
	OPENCV_IMGPROC_API float* get_map_x1(opencv_wrapper* opencv_wrapper_ptr);
	OPENCV_IMGPROC_API float* get_map_y1(opencv_wrapper* opencv_wrapper_ptr);
	OPENCV_IMGPROC_API float* get_map_x2(opencv_wrapper* opencv_wrapper_ptr);
	OPENCV_IMGPROC_API float* get_map_y2(opencv_wrapper* opencv_wrapper_ptr);
}