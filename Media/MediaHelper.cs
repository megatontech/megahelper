#region Copyright © www.ef-automation.com 2015. All right reserved.

// ----------USER  : yu

#endregion Copyright © www.ef-automation.com 2015. All right reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace BaseUtils
{
    public class MediaHelper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MediaHelper()
        {
        }

        #region 同步播放wav文件

        /// <summary>
        /// 播放声音
        /// </summary>
        private void playSound(System.IO.Stream stream)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(stream);
            player.Load();
            player.Play();
        }

        /// <summary>
        /// 以同步方式播放wav文件
        /// </summary>
        /// <param name="sp">SoundPlayer对象</param>
        /// <param name="wavFilePath">wav文件的路径</param>
        public static void SyncPlayWAV(SoundPlayer sp, string wavFilePath)
        {
            try
            {
                //设置wav文件的路径
                sp.SoundLocation = wavFilePath;

                //使用异步方式加载wav文件
                sp.LoadAsync();

                //使用同步方式播放wav文件
                if (sp.IsLoadCompleted)
                {
                    sp.PlaySync();
                }
            }
            catch (Exception ex)
            {
                string errStr = ex.Message;
                throw ex;
            }
        }

        /// <summary>
        /// 以同步方式播放wav文件
        /// </summary>
        /// <param name="wavFilePath">wav文件的路径</param>
        public static void SyncPlayWAV(string wavFilePath)
        {
            try
            {
                //创建一个SoundPlaryer类，并设置wav文件的路径
                SoundPlayer sp = new SoundPlayer(wavFilePath);

                //使用异步方式加载wav文件
                sp.LoadAsync();

                //使用同步方式播放wav文件
                if (sp.IsLoadCompleted)
                {
                    sp.PlaySync();
                }
            }
            catch (Exception ex)
            {
                string errStr = ex.Message;
                throw ex;
            }
        }

        #endregion 同步播放wav文件

        #region 异步播放wav文件

        /// <summary>
        /// 以异步方式播放wav文件
        /// </summary>
        /// <param name="sp">SoundPlayer对象</param>
        /// <param name="wavFilePath">wav文件的路径</param>
        public static void ASyncPlayWAV(SoundPlayer sp, string wavFilePath)
        {
            try
            {
                //设置wav文件的路径
                sp.SoundLocation = wavFilePath;

                //使用异步方式加载wav文件
                sp.LoadAsync();

                //使用异步方式播放wav文件
                if (sp.IsLoadCompleted)
                {
                    sp.Play();
                }
            }
            catch (Exception ex)
            {
                string errStr = ex.Message;
                throw ex;
            }
        }

        /// <summary>
        /// 以异步方式播放wav文件
        /// </summary>
        /// <param name="wavFilePath">wav文件的路径</param>
        public static void ASyncPlayWAV(string wavFilePath)
        {
            try
            {
                //创建一个SoundPlaryer类，并设置wav文件的路径
                SoundPlayer sp = new SoundPlayer(wavFilePath);

                //使用异步方式加载wav文件
                sp.LoadAsync();

                //使用异步方式播放wav文件
                if (sp.IsLoadCompleted)
                {
                    sp.Play();
                }
            }
            catch (Exception ex)
            {
                string errStr = ex.Message;
                throw ex;
            }
        }

        #endregion 异步播放wav文件

        #region 停止播放wav文件

        /// <summary>
        ///
        /// </summary>
        /// <param name="sp">SoundPlayer对象</param>
        public static void StopWAV(SoundPlayer sp)
        {
            sp.Stop();
        }

        #endregion 停止播放wav文件
    }
}