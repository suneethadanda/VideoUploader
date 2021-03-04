using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using stdole;
using CAVEditLib;

namespace WpfVideoUploader
{
    public partial class OutputOptions
    {

        //private const int CONFIGURATION_1 = 1;
        //private const int CONFIGURATION_2 = 2;
        //private const int CONFIGURATION_3 = 3;
        //private int mCurrconfigl;

        private const int AV_TIME_BASE = 1000000;
        private static ulong AV_NOPTS_VALUE = 0x8000000000000000;
        private const string VIDEO_FILTER_DVD = ".vcd.svcd.dvd";

        private const string HOOK_PATH = "\\vhook\\";
        private const string IMLIB2_DLL = "imlib2.dll";
        private const string IMLIB2_IMG = "imlib2.jpg";
        private const string IMLIB2_DELPHI_DLL = "libImlib2-1.dll";
        private const string FREE_TYPE_DLL = "libfreetype-6.dll";
        private const string DELPHI_HOOK_DLL = "DelphiHook.dll";
        private const string DELPHI_HOOK_IMG = "DelphiHook.bmp";

        private enum HOOK_TYPE { NONE, IMLIB2, DELPHI };

        private string[] OUTPUT_FORMATS = new string[] {  
                                                        "AVI",
                                                        "MOV",
                                                        "RM10",
                                                        "M1V",
                                                        "MP2",
                                                        "SVCD",
                                                        "FLV",
                                                        "MP4",
                                                        "RM20",
                                                        "M2V",
                                                        "MP3",
                                                        "DVD",
                                                        "iPod",
                                                        "OGG",
                                                        "SWF",
                                                        "AAC",
                                                        "WAV",
                                                        "3GP",
                                                        "MKV",
                                                        "PSP",
                                                        "WMV",
                                                        "AC3",
                                                        "WMA",
                                                        "MPG",
                                                        "MTS"};

        private static string[] AUDIO_BITRATES = new string[] {
            "Auto - Original", "32", "64", "128", "192"
        };

        private ICAVConverter mConverter = null;
        private long mDuration = 0;
        private int mFrames = 0;
        private long mFirstPTS = long.MaxValue;
        private long mStartTime = long.MinValue;
        private long mEndTime = (long)AV_NOPTS_VALUE;

        // NOTICE: this demo application only shows first video stream &&/or first audio stream.
        private int mVideoStreamIndex = 0;
        private int mAudioStreamIndex = 0;

        private const int CONFIGURATION_1 = 1;
        private const int CONFIGURATION_2 = 2;
        private const int CONFIGURATION_3 = 3;

        private int mCurrconfig;

       private HOOK_TYPE mHookType = HOOK_TYPE.NONE;

        public OutputOptions(int currConfig)
        {
            string hookDLL = Application.StartupPath + HOOK_PATH + DELPHI_HOOK_DLL;
            if (File.Exists(hookDLL))
            {
                mHookType = HOOK_TYPE.DELPHI;
            }

            hookDLL = Application.StartupPath + HOOK_PATH + IMLIB2_DLL;

            if (File.Exists(hookDLL))
            {
                mHookType = HOOK_TYPE.IMLIB2;
            }
            mCurrconfig = currConfig;
        }

       

        public void SetConverter(ICAVConverter converter)
        {
            try
            {
                mConverter = converter;
                // NOTICE: this demo application only shows first video stream &&/or first audio stream.
                mVideoStreamIndex = mConverter.AVPrope.FirstVideoStreamIndex;
                mAudioStreamIndex = mConverter.AVPrope.FirstAudioStreamIndex;

                // file duration
                if (mConverter.AVPrope.FileStreamInfo.Duration != (long)AV_NOPTS_VALUE)
                    mDuration = mConverter.AVPrope.FileStreamInfo.Duration;
                    
                // calculate Frames of the first video stream, use it for the track bar max position
                if ((mConverter.AVPrope.VideoStreamCount > 0) && (mConverter.AVPrope.FirstVideoStreamInfo.FrameRate.den > 0))
                {
                    if (mConverter.AVPrope.FirstVideoStreamInfo.DurationScaled > 0)
                        mFrames = (int)Math.Round((double)mConverter.AVPrope.FirstVideoStreamInfo.DurationScaled / AV_TIME_BASE *
                                    mConverter.AVPrope.FirstVideoStreamInfo.FrameRate.num / mConverter.AVPrope.FirstVideoStreamInfo.FrameRate.den);
                    else if (mConverter.AVPrope.FileStreamInfo.Duration != (long)AV_NOPTS_VALUE)
                        mFrames = (int)Math.Round((double)mConverter.AVPrope.FileStreamInfo.Duration / AV_TIME_BASE *
                                    mConverter.AVPrope.FirstVideoStreamInfo.FrameRate.num / mConverter.AVPrope.FirstVideoStreamInfo.FrameRate.den);
                }
                else if (mConverter.AVPrope.FileStreamInfo.Duration != (long)AV_NOPTS_VALUE)
                    mFrames = (int)Math.Round((double)mConverter.AVPrope.FileStreamInfo.Duration / AV_TIME_BASE * 10);
            }
            catch (Exception ex)
            {
                Common.WriteLog("Problem in Set Converter: " + ex.Message);
            }
        }

        public void GetInputOptions()
        {
            try
            {
                ICInputOptions inputOptions = mConverter.InputOptions;

                inputOptions.Init();

                // please refer to the relational document for detail information of ICInputOptions

                inputOptions.FileName = mConverter.AVPrope.FileName;
                inputOptions.FileFormat = mConverter.AVPrope.ForceFormat; // !!!*RECOMMEND*!!!


                // cut a piece clip, see also outputOptions.TimeLength
#if _QUICK_CUT
            if (mEndTime > mStartTime)
                inputOptions.TimeStart = GetClipStartTime(); // start time offset
#endif
            }
            catch (Exception ex)
            {
                Common.WriteLog("Problem in Input Option:" + ex.Message); 
            }
        }

        public void GetOutputOptions(int targetVideoWidth, int targetVideoHeight)
        {
            try
            {
                ICOutputOptions outputOptions = mConverter.OutputOptions;
                
                string fileExt;

                outputOptions.Init();

                // here we use FileExt to indicate the output format
                //fileExt = "." + "flv";
                fileExt = "." + "mp4";

                // Audio Volume          
                outputOptions.AudioVolume = Common.StrToNumberWithDefValue("256", -1);

                // Video Bit Rate: in bits/s, bps!!! not Kbps!!! so multiply 1000
                outputOptions.VideoBitrate = mConverter.AVPrope.FirstVideoStreamInfo.BitRate;
                

                // Audio Bit Rate: in bits/s, bps!!! not Kbps!!! so multiply 1000
                //outputOptions.AudioBitrate = mConverter.AVPrope.FirstAudioStreamInfo.BitRate;
                outputOptions.AudioBitrate = 1000 * Common.StrToNumberWithDefValue("128", 64);


                // cut a piece clip
#if !_QUICK_CUT
                if (mEndTime > mStartTime)
                    outputOptions.TimeStart = GetClipStartTime(); // start time offset
#endif


                // cut a piece clip, see also IO.StartTime
                if (mEndTime > mStartTime)
                    outputOptions.TimeLength = GetClipTimeLength(); // duration of the new piece clip


                // special options depend on output formats
                if ((fileExt == ".flv") || (fileExt == ".swf") || (fileExt == ".mp4"))
                {
                    //outputOptions.VideoBitrate = 4142000;
                    outputOptions.VideoBitrate = 2142000;
                    outputOptions.FrameRate = "24";//"640x360";//"1280x720";//
                    outputOptions.AudioCodec = "libmp3lame";

                    //outputOptions.VideoCodec = "mpeg4";   //{Do not Localize}
                    //outputOptions.VideoPreset = "libx264-default";
                    
                    outputOptions.AudioChannels = 2;

                    // FLV/SWF only supports this three sample rate, (44100, 22050, 11025).
                    if ((outputOptions.AudioSampleRate != 11025) && (outputOptions.AudioSampleRate != 22050) && (outputOptions.AudioSampleRate != 44100))
                        outputOptions.AudioSampleRate = 22050;
                }
                else if (fileExt == ".ogg")
                {
                    outputOptions.AudioCodec = "libvorbis"; //{Do not Localize}
                    // Ogg with video
                    outputOptions.VideoCodec = "libtheora"; //{Do not Localize}
                    // Ogg Vorbis Audio only
                    //outputOptions.DisableVideo = True;
                }
                else if (fileExt == ".psp")
                {
                    /*
                    please refer to http://ffmpeg.mplayerhq.hu/faq.html#SEC26

                    3.14 How do I encode videos which play on the PSP?
                    `needed stuff'
                    -acodec libfaac -vcodec mpeg4 width*height<=76800 width%16=0 height%16=0 -ar 24000 -r 30000/1001 or 15000/1001 -f psp
                    `working stuff'
                        4mv, title
                    `non-working stuff'
                        B-frames
                    `example command line'
                        ffmpeg -i input -acodec libfaac -ab 128kb -vcodec mpeg4 -b 1200kb -ar 24000 -mbd 2 -flags +4mv+trell -aic 2 -cmp 2 -subcmp 2 -s 368x192 -r 30000/1001 -title X -f psp output.mp4
                    `needed stuff for H.264'
                        -acodec libfaac -vcodec libx264 width*height<=76800 width%16=0? height%16=0? -ar 48000 -coder 1 -r 30000/1001 or 15000/1001 -f psp
                    `working stuff for H.264'
                        title, loop filter
                    `non-working stuff for H.264'
                        CAVLC
                    `example command line'
                        ffmpeg -i input -acodec libfaac -ab 128kb -vcodec libx264 -b 1200kb -ar 48000 -mbd 2 -coder 1 -cmp 2 -subcmp 2 -s 368x192 -r 30000/1001 -title X -f psp -flags loop -trellis 2 -partitions parti4x4+parti8x8+partp4x4+partp8x8+partb8x8 output.mp4
                    `higher resolution for newer PSP firmwares, width<=480, height<=272'
                        -vcodec libx264 -level 21 -coder 1 -f psp
                    `example command line'
                        ffmpeg -i input -acodec libfaac -ab 128kb -ac 2 -ar 48000 -vcodec libx264 -level 21 -b 640kb -coder 1 -f psp -flags +loop -trellis 2 -partitions +parti4x4+parti8x8+partp4x4+partp8x8+partb8x8 -g 250 -s 480x272 output.mp4
                    */
                    outputOptions.FileFormat = "psp";       //{Do not Localize}
                    outputOptions.VideoCodec = "mpeg4";     //{Do not Localize}
                    outputOptions.AudioCodec = "libfaac";   //{Do not Localize}
                    outputOptions.AudioSampleRate = 24000;
                   // outputOptions.AudioSampleRate = 10171000;
                    outputOptions.VideoBitrate = 10171000;
                    outputOptions.FrameRate = "15000/1001";
                    fileExt = "_psp.mp4";
                }
                else if (fileExt == ".ipod")
                {
                    /*
                    please refer to http://ffmpeg.mplayerhq.hu/faq.html#SEC25

                    3.13 How do I encode videos which play on the iPod?
                    `needed stuff'
                        -acodec libfaac -vcodec mpeg4 width<=320 height<=240
                    `working stuff'
                        4mv, title
                    `non-working stuff'
                        B-frames
                    `example command line'
                        ffmpeg -i input -acodec libfaac -ab 128kb -vcodec mpeg4 -b 1200kb -mbd 2 -flags +4mv+trell -aic 2 -cmp 2 -subcmp 2 -s 320x180 -title X output.mp4
                    */
                    outputOptions.VideoCodec = "mpeg4";     //{Do not Localize}
                    outputOptions.AudioCodec = "libfaac";   //{Do not Localize}
                    outputOptions.FrameSize = "320x240";
                    fileExt = "_ipod.mp4";
                }
                else if (fileExt == ".rm10")
                {
                    outputOptions.VideoCodec = "rv10";      //{Do not Localize}
                    fileExt = "_rv10.rm";
                }
                else if (fileExt == ".rm20")
                {
                    outputOptions.VideoCodec = "rv20";      //{Do not Localize}
                    fileExt = "_rv20.rm";
                }
                else if (fileExt == ".mp4")
                {
                    outputOptions.AudioChannels = 1;
                    outputOptions.AudioSampleRate = 16000;
                    outputOptions.FrameRate = "15";
                    outputOptions.FrameAspectRatio = "1.222";
                }
                else if (fileExt == ".wmv")
                {
                    outputOptions.VideoCodec = "wmv2";      //{Do not Localize}
                    outputOptions.AudioCodec = "wmav2";     //{Do not Localize}
                    //outputOptions.VideoBitrate = 256 * 1024;
                }
                else if (fileExt == ".mkv")
                {
                    outputOptions.VideoCodec = "libx264";   //{Do not Localize}
                    outputOptions.VideoPreset = "libx264-default";
                    //outputOptions.AudioCodec = 'ac3';       //{Do not Localize}
                }
                else if (fileExt == ".wma")
                {
                    outputOptions.DisableVideo = true;
                    outputOptions.AudioCodec = "wmav2";     //{Do not Localize}
                }
                else if (fileExt == ".3gp")
                {
                    // Only mono supported
                    outputOptions.AudioChannels = 1;
                    // Only 8000Hz sample rate supported
                    outputOptions.AudioSampleRate = 8000;
                    // bitrate not supported: use one of
                    //    4.75k, 5.15k, 5.9k, 6.7k, 7.4k, 7.95k, 10.2k or 12.2k
                    /*
                    AMR_bitrates rates[]={ {4750,MR475},
                                           {5150,MR515},
                                           {5900,MR59},
                                           {6700,MR67},
                                           {7400,MR74},
                                           {7950,MR795},
                                           {10200,MR102},
                                           {12200,MR122},
                                                 };
                    */
                    outputOptions.AudioBitrate = 7400;
                    // [h263 @ 6BF9AE00]The specified picture size of 320x240 is not valid for the H.263 codec.
                    //    Valid sizes are 128x96, 176x144, 352x288, 704x576, && 1408x1152. Try H.263+.
                    outputOptions.FrameSize = "128x96";
                }
                else if (VIDEO_FILTER_DVD.IndexOf(fileExt) > 0)
                {
                    if (fileExt == ".vcd")
                        outputOptions.TargetType = COptionTargetType.cttVCD;
                    else if (fileExt == ".svcd")
                        outputOptions.TargetType = COptionTargetType.cttSVCD;
                    else // if Ext = '.dvd' then
                        outputOptions.TargetType = COptionTargetType.cttDVD;

                    outputOptions.NormType = COptionNormType.cntAuto;

                    outputOptions.NormDefault = (int)COptionNormType.cntPAL; // or ntNTSC, according the locale region
                }

                outputOptions.Info.TimeStamp = DateTime.Now; // 0 means current time: Now()
                {
                    /*
                    outputOptions.Info.Title = "cc title";
                    outputOptions.Info.Author = "cc author";
                    outputOptions.Info.Copyright = "cc copyright";
                    outputOptions.Info.Comment = "cc comment";
                    outputOptions.Info.Album = "cc album";
                    outputOptions.Info.Year = 2008;
                    outputOptions.Info.Track = 5;
                    outputOptions.Info.Genre = "Dance Hall";
                    */
                }


                {
                    outputOptions.AudioLanguage = "eng";
                    outputOptions.SubtitleLanguage = "eng";
                }

                // TODO: more options

                // change vcd/svcd/dvd file ext to .mpg
                if (VIDEO_FILTER_DVD.IndexOf(fileExt) > 0)
                {
                    fileExt = "_" + fileExt.Substring(1);
                    if (outputOptions.NormType == COptionNormType.cntPAL)
                        fileExt = fileExt + "_pal.mpg";
                    else if (outputOptions.NormType == COptionNormType.cntNTSC)
                        fileExt = fileExt + "_ntsc.mpg";
                    else
                        fileExt = fileExt + "_auto.mpg";
                }

                // generate output filename automatically
                //string fileNanme = mConverter.AVPrope.FileName;

                outputOptions.FileExt = fileExt;

                // NOTICE: the component will use OutputFileName's FileExt to guess
                //         the output format when IO.FileFormat isn't defined.

                outputOptions.DisableSubtitle = true;

                // Padd or Crop depending on the Target and Source Hieght
                Common.WriteLog("GetOutputOptions: Target Video Frame = " + targetVideoWidth + "x" + targetVideoHeight);
                int sourceVideoHeight = mConverter.AVPrope.FirstVideoStreamInfo.Height;


                if (targetVideoHeight != sourceVideoHeight)
                {
                    AddPaddingCropping(targetVideoWidth, targetVideoHeight, outputOptions);
                }
            }
            catch(Exception ex)
            {
                Common.WriteLog("GetOutputOptions: " + ex.Message);
            }
        }

        /// <summary>
        /// Calculate the ration required to Padding/Cropping
        /// Padd/Crop Top and Botom (even number) depending on the height condition
        /// by dividing the calculated values
        /// </summary>
        /// <param name="TargetVideoWidth"></param>
        /// <param name="TargetVideoHeight"></param>
        /// <param name="outputOptions"></param>
        private void AddPaddingCropping(double TargetVideoWidth, double TargetVideoHeight, ICOutputOptions outputOptions)
        {
            int paddingTop = 0;
            int paddingBotom = 0;
            int diff = 0;

            double sourceVideoWidth = mConverter.AVPrope.FirstVideoStreamInfo.Width;
            double sourceVideoHeight = mConverter.AVPrope.FirstVideoStreamInfo.Height;

            Common.WriteLog("AddPaddingCropping: Source Video Frame = " + sourceVideoWidth + "x" + sourceVideoHeight);

            double aspectRatio = sourceVideoWidth / sourceVideoHeight;

            double tempVideoHeight = Math.Round(TargetVideoWidth / aspectRatio);

            if (tempVideoHeight < TargetVideoHeight)
                diff = Convert.ToInt32(TargetVideoHeight - tempVideoHeight);
            else
                diff = Convert.ToInt32(tempVideoHeight - TargetVideoHeight);
         
            int dPad = Convert.ToInt32(diff / 2);

            paddingTop = (dPad % 2 != 0) ? (dPad + 1) : dPad;
            paddingBotom = Convert.ToInt32( TargetVideoHeight - tempVideoHeight - paddingTop);

            if (paddingBotom % 2 != 0)
            {
                paddingBotom--;
                paddingTop++;
            }

            if (tempVideoHeight < TargetVideoHeight)
            {
                outputOptions.PadTop = paddingTop;
                outputOptions.PadBottom = paddingBotom;
            }
            else
            {
                outputOptions.CropTop = paddingTop;
                outputOptions.CropBottom = paddingBotom;
            }
       
            outputOptions.FrameSize = String.Format("{0}X{1}", TargetVideoWidth, tempVideoHeight);
        }

        private Int64 GetClipStartTime()
        {
            Int64 result = 0;

            // start time offset
            if ((mFirstPTS != Int64.MaxValue) && (mFirstPTS < 0))
                result = mStartTime - mFirstPTS;
            else
                result = mStartTime;

            return result;
        }

        private Int64 GetClipTimeLength()
        {
            return mEndTime - mStartTime;
        }
    }
}
