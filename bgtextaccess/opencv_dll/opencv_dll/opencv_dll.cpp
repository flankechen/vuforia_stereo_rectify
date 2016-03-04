

#include "opencv_dll.h"

extern "C" {

	//void constructor
	opencv_wrapper::opencv_wrapper()
	{

	}

	opencv_wrapper::~opencv_wrapper()
	{

	}

	bool opencv_wrapper::read_calib()
	{
		cv::FileStorage fs_calb_para("./save_param/calib_para.yml", CV_STORAGE_READ);
		if(!fs_calb_para.isOpened())
		{
			return false;
		}
		else
		{
			fs_calb_para["MX1"] >> mx1;
			fs_calb_para["MX2"] >> mx2;
			fs_calb_para["MY1"] >> my1;
			fs_calb_para["MY2"] >> my2;
			fs_calb_para.release();
		}

		cv::convertMaps(mx1, my1, mx1_f, my1_f, CV_32FC1);
		cv::convertMaps(mx2, my2, mx2_f, my2_f, CV_32FC1);


		return true;
	}

	opencv_wrapper* _cdecl create_opencv_wrapper()
	{
		opencv_wrapper* opencv_wrapper_ptr = new opencv_wrapper();
		if(opencv_wrapper_ptr->read_calib())
			return opencv_wrapper_ptr;
		else
			return NULL;
	}

	float* get_map_x1(opencv_wrapper* opencv_wrapper_ptr)
	{
		return (float*)opencv_wrapper_ptr->mx1_f.data;
	}

	float* get_map_y1(opencv_wrapper* opencv_wrapper_ptr)
	{
		return (float*)opencv_wrapper_ptr->my1_f.data;
	}

	float* get_map_x2(opencv_wrapper* opencv_wrapper_ptr)
	{
		return (float*)opencv_wrapper_ptr->mx2_f.data;
	}

	float* get_map_y2(opencv_wrapper* opencv_wrapper_ptr)
	{
		return (float*)opencv_wrapper_ptr->my2_f.data;
	}

}