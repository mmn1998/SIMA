using MD.PersianDateTime;
using Portable.Licensing;
using System.Text;

namespace SIMA.LicenseKeyGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_CreateLicense_Click(object sender, EventArgs e)
        {
            var domain = txt_Domain.Text;

            var version = txt_Version.Text;

            var date = PersianDateTime.Parse(msk_date.Text).ToDateTime();

            var hasIssueManagement = chk_Issue.Checked;

            var hasWorkFlow = chk_WorkFlow.Checked;

            var featureDictionery = new Dictionary<string, string>

{

    { "IssueManagement".ToLower(), hasIssueManagement.ToString() },

    { "WorkFlows".ToLower(), hasWorkFlow.ToString() },

    { "Domain".ToLower(), domain },

    {"version", version },



};

            var passPharase = txt_PassPharase.Text;



            var keyGenerator = Portable.Licensing.Security.Cryptography.KeyGenerator.Create();

            var keyPair = keyGenerator.GenerateKeyPair();

            var privateKey = keyPair.ToEncryptedPrivateKeyString(passPharase);

            var publicKey = keyPair.ToPublicKeyString();

            var license = License.New()

                                .WithUniqueIdentifier(Guid.NewGuid())

                                .As(LicenseType.Standard)

                                .ExpiresAt(date)

                                .WithProductFeatures(featureDictionery)

                                .LicensedTo("Sima", "m.valizadeh@gmail.com")

                                .CreateAndSignWithPrivateKey(privateKey, passPharase);

            rtxt_License.Text = license.ToString();

            rtxt_PublicKey.Text = publicKey;

            File.WriteAllText("License.lic", license.ToString(), Encoding.UTF8);

            MessageBox.Show("فایل لایسنس ایجاد شد");
        }
    }
}
